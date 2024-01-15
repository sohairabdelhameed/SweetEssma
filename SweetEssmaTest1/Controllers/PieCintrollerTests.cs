using EssmaCars.Controllers;
using EssmaCars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SweetEssmaTest1.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetEssmaTest1.Controllers
{
    public class PieCintrollerTests
    {
        [Fact]
        public void List_EmptyCategory_ReturnAllPies()
        {
            //arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var pieController = new PieController(mockPieRepository.Object
                , mockCategoryRepository.Object);

            //act 
            var result = pieController.List("");

            //assert
            var ViewResults = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(ViewResults.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }

    }
}
