using Workshopmanagement.DatabaseManagment.DataObject;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Globalization;
using AKSoftware.Localization.MultiLanguages;
using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment;
using HtmlAgilityPack;
using static MudBlazor.CategoryTypes;
using System.Text.RegularExpressions;
using VetCV.HtmlRendererCore.PdfSharpCore;
using PdfSharpCore;
using PdfSharpCore.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Workshopmanagement.Utility
{
    public static class Utility
    {
        public static List<string> imageBasePath = GetPathFromEnv("ImageBasePath", ["wwwroot","images","upload"]);
        public static string templateBasePath = Environment.GetEnvironmentVariable("TemplateBasePath") ?? @"Templates";
        public static List<string> templateSavePath = GetPathFromEnv("TemplateSavePath", ["wwwroot","templates"]);

        private static bool _fontResolverRegistered = false;
        private static readonly object _fontResolverLock = new();

        public static int[] pageSizeOptions = [10, 20, 50];

        public enum DateFormat
        {
            OnlyDate,
            OnlyDateExtended,
            FullDate,
            FullDateExtended,
            OnlyTime,
            OnlyTimeExtended
        }

        public static async Task<bool> IsUserLoggedInAsync(ProtectedLocalStorage protectedLocalStorage)
        {
            var localStorageData = await protectedLocalStorage.GetAsync<string>(nameof(Employee));

            if(!localStorageData.Success)
                return false;
            if (String.IsNullOrEmpty(localStorageData.Value))
                return false;
            if (String.IsNullOrEmpty(localStorageData.Value.Replace("null", "")))
                return false;
            if (JsonConvert.DeserializeObject<Employee>(localStorageData.Value).EmployeeRole == EmployeeRole.None)
                return false;

            var localStorageDate = await protectedLocalStorage.GetAsync<string>(nameof(DateTime));
            if (!localStorageDate.Success)
                return false;
            if (String.IsNullOrEmpty(localStorageDate.Value))
                return false;

            if (DateTime.Now.Date != JsonConvert.DeserializeObject<DateTime>(localStorageDate.Value).Date) //Login 1 volta al giorno
            {
                await Utility.SaveCurrentLoggedEmployee(protectedLocalStorage, null);
                return false;
            }

            return true;
            
        }

        public static async Task<Employee> GetCurrentLoggedEmployee(ProtectedLocalStorage protectedLocalStorage)
        {
            if(!(await IsUserLoggedInAsync(protectedLocalStorage)))
                return new Employee();

            var localStorageData = await protectedLocalStorage.GetAsync<string>(nameof(Employee));
            #pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<Employee>(localStorageData.Value);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public static async Task SaveCurrentLoggedEmployee(ProtectedLocalStorage protectedLocalStorage, Employee employee)
        {
            if (employee == null)
                await protectedLocalStorage.DeleteAsync(nameof(Employee));

            await protectedLocalStorage.SetAsync(nameof(Employee), JsonConvert.SerializeObject(employee));
            await protectedLocalStorage.SetAsync(nameof(DateTime), JsonConvert.SerializeObject(DateTime.Now));
        }

        public static string GetPhoneNumberHtml(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) { return ""; }
            phoneNumber = phoneNumber.Trim();
            if (!phoneNumber.StartsWith("+")) phoneNumber = $"+{phoneNumber}";
            return $"tel:{phoneNumber}";
        }

        public static string GetMailHtml(string mail)
        {
            if (string.IsNullOrEmpty(mail)) { return ""; }
            mail = mail.Trim();
            return $"mailto:{mail}";
        }

        public static string TruncateFroHtml(string? value, int maxValue = 250)
        {
            if (String.IsNullOrEmpty(value))
                return "";

            if (value.Length < maxValue)
                return value;

            return $"{value.Substring(0, maxValue)} ...";
        }

        public static string GetDateFormatted(DateTime? dateTime, DateFormat dateFormat)
        {          
            if (!dateTime.HasValue)
                return "";

            var cultureS = $"{Environment.GetEnvironmentVariable("WebClientCulture")}";
            if (String.IsNullOrEmpty(cultureS))
                cultureS = "it-IT";

            var culture = CultureInfo.GetCultureInfo(cultureS);

            switch (dateFormat)
            {
                case DateFormat.OnlyDate:
                    return dateTime.Value.ToString("d", culture); //6/15/2008
                case DateFormat.OnlyDateExtended:
                    return dateTime.Value.ToString("D", culture); //Sunday, June 15, 2008
                case DateFormat.FullDate:
                    return dateTime.Value.ToString("G", culture); //6/15/2008 9:15:07 PM
                case DateFormat.FullDateExtended:
                    return dateTime.Value.ToString("F", culture); //Sunday, June 15, 2008 9:15:07 PM
                case DateFormat.OnlyTime:
                    return dateTime.Value.ToString("t", culture); //9:15 PM
                case DateFormat.OnlyTimeExtended:
                    return dateTime.Value.ToString("T", culture); //9:15:07 PM
                default:
                    return dateTime.Value.ToString(culture);
            }
        }

        public static string GetImagePath(string imageUid, string excecutionUid, string fileName)
        {
            
            var extension = Path.GetExtension(fileName);
            var paths = CloneOject<List<string>>(imageBasePath);
            paths.Add(excecutionUid);
            var path = Path.Combine(paths.ToArray());
            var pathAndName = Path.Combine(path, $"{imageUid}{extension}");

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return pathAndName;
        }

        public static string GetDescriptionFromVeichle(Veichle veichle, ILanguageContainerService languageContainerService)
        {
            if (veichle == null)
                return "";

            var baseS = $"{veichle.LicensePlate}";
            if (!String.IsNullOrEmpty(veichle.Model))
                baseS = $"{baseS} - {veichle.Model}";

            var openCount = veichle.OpenWorkflows?.Count ?? 0;
            if (openCount > 0)
                return $"{baseS} - {openCount} {languageContainerService.Keys["VeichleOpenWorkflowCount"]}";
            else
                return $"{baseS} ";
        }

        public static string GetDescriptionFromCustomer(Customer? customer, ILanguageContainerService languageContainerService)
        {
            var itm = $"{customer?.Name} {customer?.Surname}";

            if (customer?.IsCompany ?? false)
                itm = $"{itm} - {customer?.BusinessName} - {customer?.VatNumber}";
            else
                itm = $"{itm} - {customer?.FiscalCode}";

            return itm;
        }

        public static async Task<string> GetWorflowTemplate()
        {
            var templatePath = Path.Combine(templateBasePath, @"workflowTemplateBase.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException(templatePath);

            return await File.ReadAllTextAsync(templatePath);
        }

        public static async Task<string> FillTemplate(ILanguageContainerService languageContainer,
                                                      Employee employee,
                                                      Customer customer,
                                                      Veichle veichle,
                                                      Workflow workflow,
                                                      IEnumerable<DatabaseManagment.DataObject.WorkflowTask> workflowTasks,
                                                      IEnumerable<DatabaseManagment.DataObject.WorkflowExecution> workflowExecutions,
                                                      IDbDependency dbDependency)
        {
            var reportTemplate = (await GetWorflowTemplate()) ?? "";

            #region Save and remove the sign task
            //Serve cosi non sivede nei dettagli
            var signTask = workflowTasks.FirstOrDefault(x => x.IsSignTask == true);
            WorkflowExecution signExTask = new WorkflowExecution();
            if (signTask != null)
            {
                if (signExTask.IsDisclamerAleadySign){
                    //In caso ha già firmato prendo l'ultima firma valida prima di questo task
                    var signExecution = await dbDependency.GetDataAccessWorkflowExecution().FindAsync(new SearcherWorkflowExecution()
                    {
                        UserId = workflowExecutions?.FirstOrDefault()?.UserId ?? "00000",
                        VeichleId = workflowExecutions?.FirstOrDefault()?.VeichleId ?? "00000"
                    });
                    signExTask = signExecution?.Where(x => !String.IsNullOrEmpty(x.DisclamierSign))?.OrderByDescending(x => x.DisclamierSignDate)?.FirstOrDefault() ?? new WorkflowExecution();
                }
                else
                {
                    signExTask = workflowExecutions?.FirstOrDefault(x => x.WorkflowTaskId.Equals(signTask.Id)) ?? new WorkflowExecution();
                }
            }              

            workflowTasks = workflowTasks?.Where(x => x.IsSignTask == false) ?? new List<WorkflowTask>(); 
            var workflowTasksIds = workflowTasks.Select(x => x.Id).ToList();
            workflowExecutions = workflowExecutions?.Where(x => workflowTasksIds.Contains(x.WorkflowTaskId)) ?? new List<WorkflowExecution>();
            #endregion

            #region Titles
            reportTemplate = reportTemplate.Replace("{{ReportTitle}}", languageContainer.Keys["ReportTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationTitle}}", languageContainer.Keys["ReportCustomerInformationTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationNameTitle}}", languageContainer.Keys["ReportCustomerInformationNameTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationAddressTitle}}", languageContainer.Keys["ReportCustomerInformationAddressTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationVatNumberTitle}}", languageContainer.Keys["ReportCustomerInformationVatNumberTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationFiscalCodeTitle}}", languageContainer.Keys["ReportCustomerInformationFiscalCodeTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationTitle}}", languageContainer.Keys["ReportVeichleInformationTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationPlateTitle}}", languageContainer.Keys["ReportVeichleInformationPlateTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationModelTitle}}", languageContainer.Keys["ReportVeichleInformationModelTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationKmTitle}}", languageContainer.Keys["ReportVeichleInformationKmTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationFuelTitle}}", languageContainer.Keys["ReportVeichleInformationFuelTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationTitle}}", languageContainer.Keys["ReportWorkflowInformationTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationDateTitle}}", languageContainer.Keys["ReportWorkflowInformationDateTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationEmployeeTitle}}", languageContainer.Keys["ReportWorkflowInformationEmployeeTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationDescriptionTitle}}", languageContainer.Keys["ReportWorkflowInformationDescriptionTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowDetailTitle}}", languageContainer.Keys["ReportWorkflowDetailTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowDetailNameTitle}}", languageContainer.Keys["ReportWorkflowDetailNameTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowDetailDescriptionTitle}}", languageContainer.Keys["ReportWorkflowDetailDescriptionTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsTitle}}", languageContainer.Keys["ReportComponentsTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsNameBarcode}}", languageContainer.Keys["ReportComponentsNameBarcode"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsNameTitle}}", languageContainer.Keys["ReportComponentsNameTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsPriceTitle}}", languageContainer.Keys["ReportComponentsPriceTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsQuantityTitle}}", languageContainer.Keys["ReportComponentsQuantityTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsUnitOfMesurmentTitle}}", languageContainer.Keys["ReportComponentsUnitOfMesurmentTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentsRowTotalTitle}}", languageContainer.Keys["ReportComponentsRowTotalTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentTotalTitle}}", languageContainer.Keys["ReportComponentTotalTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportComponentFooter}}", languageContainer.Keys["ReportComponentFooter"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowDetailStateTitle}}", languageContainer.Keys["ReportWorkflowDetailStateTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationEnigineTitle}}", languageContainer.Keys["ReportVeichleInformationEnigineTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationTitleTitle}}", languageContainer.Keys["ReportWorkflowInformationTitleTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportTimeTableTitle}}", languageContainer.Keys["ReportTimeTableTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportTimeTableEmployeeTitle}}", languageContainer.Keys["ReportTimeTableEmployeeTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportTimeTableHoursTitle}}", languageContainer.Keys["ReportTimeTableHoursTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportTimeTableTotalTitle}}", languageContainer.Keys["ReportTimeTableTotalTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationTitle}}", languageContainer.Keys["ReportCompanyInformationTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationNameTitle}}", languageContainer.Keys["ReportCompanyInformationNameTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationVatNumberTitle}}", languageContainer.Keys["ReportCompanyInformationVatNumberTitle"]);
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationPhoneTitle}}", languageContainer.Keys["ReportCompanyInformationPhoneTitle"]);
            #endregion

            #region Contents
            //Company
            var companySettingsList = await dbDependency.GetDataAccessCompanySettings().FindAsync(new SearcherCompanySettings());
            var companySettings = companySettingsList?.FirstOrDefault() ?? new CompanySettings();
            var companyLogoHtml = "";
            if (companySettings.HasLogo)
            {
                var logoFilePath = Path.Combine("wwwroot", "images", "companylogo.png");
                if (File.Exists(logoFilePath))
                {
                    var logoBytes = await File.ReadAllBytesAsync(logoFilePath);
                    var logoBase64 = Convert.ToBase64String(logoBytes);
                    companyLogoHtml = $"<img src=\"data:image/png;base64,{logoBase64}\" style=\"width:80px;height:80px;border-radius:50%;object-fit:cover;\" />";
                }
            }
            reportTemplate = reportTemplate.Replace("{{ReportCompanyLogo}}", companyLogoHtml);
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationName}}", companySettings.CompanyName ?? "");
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationVatNumber}}", companySettings.CompanyVatNumber ?? "");
            reportTemplate = reportTemplate.Replace("{{ReportCompanyInformationPhone}}", companySettings.CompanyPhone ?? "");

            //Customer
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationName}}", $"{customer.Name} {customer.Surname}");
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationAddress}}", $"{customer.Address}");
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationVatNumber}}", $"{customer.VatNumber}");
            reportTemplate = reportTemplate.Replace("{{ReportCustomerInformationFiscalCode}}", $"{customer.FiscalCode}");

            //Veichle
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationPlate}}", $"{veichle.LicensePlate}");
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationModel}}", $"{veichle.Brand} {veichle.Model}");
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationEnigine}}", $"{veichle.EngineType}");
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationKm}}", $"{veichle.ActualKilometers}");
            reportTemplate = reportTemplate.Replace("{{ReportVeichleInformationFuel}}", $"{veichle.ActualFuelPercentage}");

            //Workflow
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationTitle}}", $"{workflow.Title}");
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationDescription}}", $"{workflow.Description}");
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationEmployee}}", $"{employee.Name} {employee.Surname}");
            reportTemplate = reportTemplate.Replace("{{ReportWorkflowInformationDate}}", GetDateFormatted(workflowExecutions.Where(x => x.ClosedAt != null).Select(x => x.ClosedAt).Max(), DateFormat.OnlyDateExtended));

            #region HasOutcome
            //HasOutcome           
            var taskWithOutcome = workflowTasks?.Where(x => x.HasOutcome == true)?.Select(x => x.Id)?.ToList() ?? new List<string>();
            var excecxutionWithOutcome = workflowExecutions?.Where(x => taskWithOutcome.Contains(x.WorkflowTaskId));

            var taskTable = "";
            if(excecxutionWithOutcome != null && excecxutionWithOutcome?.Count() > 0)
            {
                foreach (var execution in excecxutionWithOutcome)
                {
                    var outcome = languageContainer.Keys[execution?.Executionoutcome == null ? "ExecutionoutcomeNone" : execution?.Executionoutcome.ToString()];

                    taskTable = $"{taskTable} <tr>";
                    taskTable = $"{taskTable} <td>{execution?.Titile}</td>";
                    taskTable = $"{taskTable} <td>{outcome}</td>";
                    taskTable = $"{taskTable} <td>{execution?.Note}</td>";
                    taskTable = $"{taskTable} </tr>";
                }
            }
            else
            {
                reportTemplate = RemoveElement(reportTemplate, "OutcomeSection");
            }
                

            reportTemplate = reportTemplate.Replace("{{ReportWorkflowDetails}}", $"{taskTable}");
            #endregion

            #region Components         
            var components = workflowExecutions.SelectMany(x => x.Components ?? new List<Component>());

            var compTable = "";
            decimal tableTot = 0;
            if (components != null && components?.Count() > 0)
            {
                foreach (var component in components)
                {
                    var total = (component?.PricePerQuantity * component?.Quanity) ?? 0;
                    tableTot = tableTot + total;

                    compTable = $"{compTable} <tr>";
                    compTable = $"{compTable} <td>{component?.Barcode ?? " "}</td>";
                    compTable = $"{compTable} <td>{component?.Name ?? " "}</td>";
                    compTable = $"{compTable} <td>{component?.PricePerQuantity ?? 0}</td>";
                    compTable = $"{compTable} <td>{component?.Quanity ?? 0}</td>";
                    compTable = $"{compTable} <td>{component?.UnitOfMeasurement ?? " "}</td>";
                    compTable = $"{compTable} <td>{total}</td>";
                    compTable = $"{compTable} </tr>";
                }
            }
            else
            {
                reportTemplate = RemoveElement(reportTemplate, "ComponentsSection");
            }                
            
            reportTemplate = reportTemplate.Replace("{{ReportComponentTotalValue}}", $"{tableTot.ToString("0.##")} €");
            reportTemplate = reportTemplate.Replace("{{ReportComponents}}", $"{compTable}");
            #endregion

            #region TimeTable
            var seriesIdForTracking = workflowExecutions.FirstOrDefault()?.WorkflowExecutionSeriesId ?? "";
            var timeTrackings = new List<TimeTracking>();
            if (!string.IsNullOrEmpty(seriesIdForTracking))
            {
                timeTrackings = await dbDependency.GetDataAccessTimeTracking().FindAsync(new SearcherTimeTracking()
                {
                    WorkflowExecutionSeriesId = seriesIdForTracking
                }) ?? new List<TimeTracking>();
            }

            if (timeTrackings.Count > 0)
            {
                var allEmployees = await dbDependency.GetDataAccessEmployee().FindAsync(new SearcherEmployee()) ?? new List<Employee>();
                var employeeDict = allEmployees.ToDictionary(e => e.Id, e => e);

                var timeTableRows = "";
                var grandTotal = TimeSpan.Zero;

                foreach (var tracking in timeTrackings)
                {
                    var empTime = tracking.GetTotalTime();
                    grandTotal += empTime;
                    employeeDict.TryGetValue(tracking.EmployeeId, out var trackedEmp);
                    var empName = trackedEmp != null ? $"{trackedEmp.Name} {trackedEmp.Surname}" : tracking.EmployeeId;
                    var empHours = (int)empTime.TotalHours;
                    var empMinutes = empTime.Minutes;
                    timeTableRows += $"<tr><td>{empName}</td><td>{empHours}h {empMinutes:D2}min</td></tr>";
                }

                var grandHours = (int)grandTotal.TotalHours;
                var grandMinutes = grandTotal.Minutes;
                var grandTotalStr = $"{grandHours}h {grandMinutes:D2}min";

                reportTemplate = reportTemplate.Replace("{{ReportTimeTableRows}}", timeTableRows);
                reportTemplate = reportTemplate.Replace("{{ReportTimeTableTotal}}", grandTotalStr);
            }
            else
            {
                reportTemplate = RemoveElement(reportTemplate, "TimeTableSection");
            }
            #endregion

            #endregion

            #region Disclaimer
            if (workflow.RequireDisclaimer)
            {                 
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimer}}", languageContainer.Keys["ReportDisclaimer"]);
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerSign}}", languageContainer.Keys["ReportDisclaimerSign"]);
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerDate}}", Utility.GetDateFormatted(signExTask.DisclamierSignDate, DateFormat.OnlyDate));
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerSignImage}}", $"<img style=\"max-width: 500px; background: #FFF\" src=\"{signExTask.DisclamierSign}\">");
            }
            else
            {
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimer}}", "");
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerSign}}", "");
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerDate}}", "");
                reportTemplate = reportTemplate.Replace("{{ReportDisclaimerSign}}", "");
                reportTemplate = RemoveElement(reportTemplate, "DisclaimerSection");
            }                
            #endregion

            return reportTemplate;
        }

        private static string RemoveElement(string html, string elementId)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var node = htmlDoc.GetElementbyId(elementId);
            node.Remove();
            return htmlDoc.DocumentNode.OuterHtml;
        }

        public static async Task DeleteOldFiles()
        {
            try
            {
                var paths = CloneOject<List<string>>(templateSavePath);
                var path = Path.Combine(paths.ToArray());

                var files = Directory.GetFiles(path);

                foreach (var file in files) {
                    if (File.GetCreationTime(file) > DateTime.Now.AddDays(1))
                        File.Delete(file);
                }
            }catch(Exception ex)
            {

            };
        }
        public static byte[] GeneratePdfBytes(string htmlContent)
        {
            // Extract CSS custom properties from :root block
            var cssVars = new Dictionary<string, string>();
            var rootMatch = Regex.Match(htmlContent, @":root\s*\{([^}]+)\}", RegexOptions.Singleline);
            if (rootMatch.Success)
            {
                foreach (Match m in Regex.Matches(rootMatch.Groups[1].Value, @"--([a-zA-Z0-9-]+)\s*:\s*([^;]+);"))
                    cssVars[$"--{m.Groups[1].Value.Trim()}"] = m.Groups[2].Value.Trim();
            }

            // Replace var(--xxx) with actual values
            htmlContent = Regex.Replace(htmlContent, @"var\(--([a-zA-Z0-9-]+)\)", m =>
            {
                var key = $"--{m.Groups[1].Value}";
                return cssVars.TryGetValue(key, out var val) ? val : m.Value;
            });

            lock (_fontResolverLock)
            {
                if (!_fontResolverRegistered)
                {
                    GlobalFontSettings.FontResolver = new PdfFontResolver();
                    _fontResolverRegistered = true;
                }
            }

            var config = new PdfGenerateConfig { PageSize = PageSize.A4 };
            config.SetMargins(15);
            var pdf = PdfGenerator.GeneratePdf(htmlContent, config);
            using var ms = new MemoryStream();
            pdf.Save(ms, false);
            return ms.ToArray();
        }

        public static async Task ResizeAndSaveLogoPngAsync(byte[] imageBytes, string outputPath)
        {
            using var inputMs = new MemoryStream(imageBytes);
            using var image = await SixLabors.ImageSharp.Image.LoadAsync(inputMs);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new SixLabors.ImageSharp.Size(512, 512),
                Mode = ResizeMode.Pad,
                PadColor = SixLabors.ImageSharp.Color.Transparent
            }));
            await image.SaveAsPngAsync(outputPath);
        }

        public static async Task<string> GeneretaFileForDownloadFile(string template)
        {
            template = template.Replace("<html>", "<html moznomarginboxes mozdisallowselectionprint>");
            template = template.Replace("<body>", "<body onload=\"window.print()\">");
            return template;
        }
        public static async Task<string> SaveFileForDownloadFile(string template)
        {
            var paths = CloneOject<List<string>>(templateSavePath);
            var path = Path.Combine(paths.ToArray());

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fullpath = Path.Combine(path, $"report_{Guid.NewGuid().ToString("N")}.html");
            await File.WriteAllTextAsync(fullpath, template); 
            return fullpath.Replace("wwwroot/", "/");
        }

        private static T CloneOject<T>(T obj) {
            if (obj == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        private static List<string> GetPathFromEnv(string envVar, List<string> defaultPath)
        {
            var value = Environment.GetEnvironmentVariable(envVar);
            if (string.IsNullOrEmpty(value))
                return defaultPath;

            return value.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToList();
        }

    }

}
