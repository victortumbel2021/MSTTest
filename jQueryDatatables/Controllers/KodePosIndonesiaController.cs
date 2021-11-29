using Data.Models;
using Data.Repository;
using jQueryDatatables.LIB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace jQueryDatatables.Controllers
{
    public class KodePosIndonesiaController : Controller
    {
        private readonly IKodePosIndonesiaRepository _kodePosIndonesiaRepository;
        public KodePosIndonesiaController(IKodePosIndonesiaRepository kodePosIndonesiaRepository)
        {
            _kodePosIndonesiaRepository = kodePosIndonesiaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var kodePosData = (from tblObj in _kodePosIndonesiaRepository.GetAll() select tblObj);

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    kodePosData = _kodePosIndonesiaRepository.GetAll().OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search and Server Side Paging with LINQ
                if (!string.IsNullOrEmpty(searchValue))
                {
                    kodePosData = kodePosData.Where(t => t.KodePos.Contains(searchValue)
                    || t.Propinsi.Contains(searchValue)
                    || t.Kabupaten.Contains(searchValue)
                    || t.Kecamatan.Contains(searchValue)
                    || t.Kelurahan.Contains(searchValue));
                }

                resultTotal = kodePosData.Count();
                var result = kodePosData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IActionResult AddEditKodePosIndonesia(int Id)
        {
            KodePosIndonesia kodePosIndonesia = new KodePosIndonesia();
            if (Id > 0) kodePosIndonesia = _kodePosIndonesiaRepository.Find(b => b.ID == Id);
            return PartialView("_kodePosIndonesiaForm", kodePosIndonesia);
        }

        [HttpPost]
        public async Task<string> Create(KodePosIndonesia kodePosIndonesia)
        {
            if (ModelState.IsValid)
            {
                if (kodePosIndonesia.ID > 0)
                {
                    //kodePosIndonesia.LastModifiedDate = DateTime.Now;
                    //kodePosIndonesia.LastUpdateUser = "Admin";
                    _kodePosIndonesiaRepository.Update(kodePosIndonesia, kodePosIndonesia.ID);
                    return "Kode Pos Updated Successfully";
                }
                else
                {
                    //kodePosIndonesia.CreatedDate = DateTime.Now;
                    //kodePosIndonesia.CreationUser = "Admin";
                    await _kodePosIndonesiaRepository.AddAsyn(kodePosIndonesia);
                    var result = await _kodePosIndonesiaRepository.SaveAsync();

                    var successMessage = "Kode Pos Created Successfully. Kode Pos: " + kodePosIndonesia.ID;
                    TempData["successAlert"] = successMessage;
                    return "Kode Pos Created Successfully";
                }
            }
            return "Failed";
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            KodePosIndonesia kodePosIndonesia = _kodePosIndonesiaRepository.Get(id);
            _kodePosIndonesiaRepository.Delete(kodePosIndonesia);
            return RedirectToAction("Index");
        }


        public FileStreamResult ExportAllDatatoCSV()
        {
            var personalInfoData = (from tblObj in _kodePosIndonesiaRepository.GetAll() select tblObj).Take(100);
            var result = Common.WriteCsvToMemory(personalInfoData);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "KodePos.csv" };
        }

    }
}
