using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CPDataService.Model;
using System.Drawing.Imaging;

namespace CPDataService.BusinessLayer
{
    public class ItemImagesBusiness : BaseBusiness
    {

        public int DeleteItemImages(long itemId)
        {
            var itemImages = context.ItemImages.Where(a => a.ItemId == itemId).ToList();
            context.ItemImages.RemoveRange(itemImages);
            return context.SaveChanges();
        }

        public int SaveItemImages(ItemImages imagePaths)
        {
            List<ItemImage> lst = new List<ItemImage>();
            if (!string.IsNullOrEmpty(imagePaths.ItemImage1))
            {
                lst.Add(new ItemImage
                {
                    ItemId = long.Parse(imagePaths.ItemId),
                    ImagePath = imagePaths.ItemImage1,
                    ImageOrder = 1
                });
            }
            if (!string.IsNullOrEmpty(imagePaths.ItemImage2))
            {
                lst.Add(new ItemImage
                {
                    ItemId = long.Parse(imagePaths.ItemId),
                    ImagePath = imagePaths.ItemImage2,
                    ImageOrder = 2
                });
            }
            if (!string.IsNullOrEmpty(imagePaths.ItemImage3))
            {
                lst.Add(new ItemImage
                {
                    ItemId = long.Parse(imagePaths.ItemId),
                    ImagePath = imagePaths.ItemImage3,
                    ImageOrder = 3
                });
            }
            if (!string.IsNullOrEmpty(imagePaths.ItemImage4))
            {
                lst.Add(new ItemImage
                {
                    ItemId = long.Parse(imagePaths.ItemId),
                    ImagePath = imagePaths.ItemImage4,
                    ImageOrder = 4
                });
            }
            if (!string.IsNullOrEmpty(imagePaths.ItemImage5))
            {
                lst.Add(new ItemImage
                {
                    ItemId = long.Parse(imagePaths.ItemId),
                    ImagePath = imagePaths.ItemImage5,
                    ImageOrder = 5
                });
            }

            this.context.ItemImages.AddRange(lst);
            return this.context.SaveChanges();

        }


        private bool ValidateItemImage(ItemImages itemImages)
        {
            bool isValidData = true;
            if (string.IsNullOrEmpty(itemImages.ItemId))
            {
                isValidData = false;
            }
            if (string.IsNullOrEmpty(itemImages.UserId))
            {
                isValidData = false;
            }
            return isValidData;
        }


    }
}