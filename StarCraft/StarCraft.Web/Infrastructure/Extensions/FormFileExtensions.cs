namespace StarCraft.Web.Infrastructure.Extensions
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public static class FormFileExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this IFormFile formfile)
        {
            using (MemoryStream memorySteam = new MemoryStream())
            {
                await formfile.CopyToAsync(memorySteam);
                return memorySteam.ToArray();
            }
        }
    }
}