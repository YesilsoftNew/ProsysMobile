using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;

namespace ProsysMobile.Helper
{
    public class VehicleTypeImageDownload
    {
        IVehicleTypeSQLiteService _vehicleTypeSQLiteService;
        public VehicleTypeImageDownload(IVehicleTypeSQLiteService vehicleTypeSQLiteService)
        {
            _vehicleTypeSQLiteService = vehicleTypeSQLiteService;
        }

        readonly HttpClient _httpClient = new HttpClient();
        public async Task ImageDownload()
        {
            ImageDownloadEvent += FirstSenkPageViewModel_ImageDownloadEvent;

            List<VehicleType> vehicleType = _vehicleTypeSQLiteService.GetVehicleType();
            vehicleType = vehicleType.Where(t => !string.IsNullOrWhiteSpace(t.ImageURL)).ToList();

            HttpClient httpClient = new HttpClient();

            foreach (var item in vehicleType)
            {
                DownloadImageAsync(GlobalSetting.Instance.WebAppLink + GlobalSetting.Instance.VehicleLayoutImageSourceExtension + item.ImageURL,
                    item.ImageURL, item.Guid, enImageDownloadType.VehicleType);
            }
        }

        async Task<bool> DownloadImageAsync(string imageUrl, string imageGuID, string itemGuid, enImageDownloadType imageDownloadType)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        byte[] xImage = await httpResponse.Content.ReadAsByteArrayAsync();

                        if (File.Exists(GlobalSetting.Instance.ImageFolder + imageGuID))
                            File.Delete(GlobalSetting.Instance.ImageFolder + imageGuID);

                        File.WriteAllBytes(GlobalSetting.Instance.ImageFolder + imageGuID, xImage);

                        ImageDownloadEvent.Invoke(itemGuid, true, imageDownloadType);
                    }
                    else
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                return false;
            }
        }

        public event ImageDownloadEventHandler ImageDownloadEvent;
        public delegate void ImageDownloadEventHandler(string ItemGuid, bool IsDownload, enImageDownloadType imageDownloadType);

        void FirstSenkPageViewModel_ImageDownloadEvent(string ItemGuid, bool IsDownload, enImageDownloadType imageDownloadType)
        {
            if (IsDownload == false)
                return;

            if (imageDownloadType == enImageDownloadType.VehicleType)
                _vehicleTypeSQLiteService.setImageDownload(ItemGuid, true);
        }
    }
}
