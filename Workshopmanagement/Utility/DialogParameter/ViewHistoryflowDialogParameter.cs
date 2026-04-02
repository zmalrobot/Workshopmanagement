using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility.DialogParameter
{
    public class ViewHistoryflowDialogParameter
    {
        public string worflowReport = "";
        public string worflowTitle = "";
        public string worflowStyle = "";
        public DateTime worflowDate = DateTime.UtcNow;
        public List<Image> images = new List<Image>();
        public List<Component> components = new List<Component>();
        public bool useComponents = false;
        public bool useImages = false;
        public bool useReports = false;
    }
}
