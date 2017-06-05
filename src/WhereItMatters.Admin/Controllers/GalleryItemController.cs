using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Admin.Services;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    public class GalleryItemController : Controller
    {
        private readonly DonationRequestRepository _requestRepository;
        private readonly IRepository<GalleryItem> _galleryItemRepository;
        private readonly IImageSaveService _imageSaveService;

        public GalleryItemController(
            IRepository<GalleryItem> galleryItemRepository,
            IImageSaveService imageSaveService)
        {
            _galleryItemRepository = galleryItemRepository;
            _imageSaveService = imageSaveService;
        }

        public async Task<IActionResult> Edit(int requestId)
        {
            ViewData["DonationRequestId"] = requestId;
            var galleryItems = await _galleryItemRepository.SearchFor(g => g.DonationRequestId == requestId).ToListAsync();
            return View("Edit", galleryItems);
        }

        public async Task<IActionResult> Save(GalleryItem galleryItem)
        {
            var imageName = await _imageSaveService.SaveImage(Request);
            galleryItem.ImageName = imageName;

            if (!string.IsNullOrEmpty(galleryItem.ImageName) || !string.IsNullOrEmpty(galleryItem.ItemUrl) || !string.IsNullOrEmpty(galleryItem.EmbeddedHtml))
            {
                if (galleryItem.Id == 0)
                {
                    await _galleryItemRepository.Insert(galleryItem);
                }
                else
                {
                    await _galleryItemRepository.Update(galleryItem);
                }
            }

            ViewData["DonationRequestId"] = galleryItem.DonationRequestId;
            var galleryItems = await _galleryItemRepository.GetAll().ToListAsync();
            return View("Edit", galleryItems);
        }

        public async Task<IActionResult> Delete(int galleryItemId, int requestId)
        {
            var itemToDelete = await _galleryItemRepository.GetById(galleryItemId);
            await _galleryItemRepository.Delete(itemToDelete);

            ViewData["DonationRequestId"] = requestId;
            var galleryItems = await _galleryItemRepository.GetAll().ToListAsync();
            return View("Edit", galleryItems);
        }
    }
}
