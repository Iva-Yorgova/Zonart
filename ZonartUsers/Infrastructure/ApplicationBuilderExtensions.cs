using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Linq;
using ZonartUsers.Data;
using ZonartUsers.Data.Models;

namespace ZonartUsers.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ZonartUsersDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ZonartUsersDbContext data)
        {
            if (data.Templates.Any())
            {
                return;
            }

            data.Templates.AddRange(new[]
            {
            new Template { Price = 100.0, Name = "Eternity", ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 120.0, Name = "Joy" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 130.0, Name = "Wisdom" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 100.0, Name = "Waves" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 140.0, Name = "Business" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 110.0, Name = "Wild" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 160.0, Name = "Peace" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"},
            new Template { Price = 170.0, Name = "Clear" , ImageUrl = "https://previews.dropbox.com/p/thumb/ABOPpTmtUG_yXqEeyKRxK4ia5mJUXae6bhrE64hI1sikAetbnwIbaWO4hNGgZJ4kkRACoCHkBJTziVP37AlxovjliBb0XLcAAJYQOK2bI1JsaPRElS9mKawBePDaPpx8Q_6ZjdrNKczCZym73KIQaP--tpzoozKtN0HmCtzERA79bgctaQbqz5MB9o8anQyhoHORbDuKl3PXZo_hxR9bbkJccN-060nA-w9_02CxVQ1VSMe7HC4b2dGkDttKQmvqyeXCO7ovSKfPd7lQk8G92VdBnan-wApWI3cplZk2P5m5-8vyTGLW-z7kz_zNH8imEkxcvvMHv-fuiHdxx_G9_Gb_a3o2VhcVUgGgaEqX3wQoQA/p.png?fv_content=true&size_mode=5"}
        });

            data.SaveChanges();
        }
    }
}


