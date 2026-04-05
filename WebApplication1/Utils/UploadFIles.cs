namespace WebApplication1.Utils
{
    public class UploadFIles
    {
       static public string UploadImage(ref string FileName, IFormFile ImageFile)
        {
            FileName = Guid.NewGuid().ToString() +
                       Path.GetExtension(FileName);

            string uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images"
            );

            string fullPath = Path.Combine(uploadsFolder, FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }
            return uploadsFolder;
        }
    }
}
