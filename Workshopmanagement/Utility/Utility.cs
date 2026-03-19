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

namespace Workshopmanagement.Utility
{
    public static class Utility
    {
        public static List<string> imageBasePath = GetPathFromEnv("ImageBasePath", ["wwwroot","images","upload"]);
        public static string templateBasePath = Environment.GetEnvironmentVariable("TemplateBasePath") ?? @"Templates";
        public static List<string> templateSavePath = GetPathFromEnv("TemplateSavePath", ["wwwroot","templates"]);

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
            #endregion

            #region Contents
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
