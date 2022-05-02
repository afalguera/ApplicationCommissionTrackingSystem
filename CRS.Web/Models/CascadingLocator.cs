
namespace CRS.Models
{
    public class CascadingLocator
    {
        #region Properties

        public bool UseChannelId { get; set; }

        public bool UseRegionId { get; set; }

        public bool UseAreaId { get; set; }

        public bool UseDistrictId { get; set; }

        public bool ExcludeBranch { get; set; }

        public bool UseBranchId { get; set; }

        #endregion
    }
}