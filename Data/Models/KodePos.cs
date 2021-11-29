using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class KodePosIndonesia 
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Kode Pos is required.")]
        [DisplayName("Kode Pos")]
        public string KodePos { get; set; }

        [Required(ErrorMessage = "Kelurahan is required.")]
        [DisplayName("Kelurahan")]
        public string Kelurahan { get; set; }

        [Required(ErrorMessage = "Kecamatan is required.")]
        [DisplayName("Kecamatan")]
        public string Kecamatan { get; set; }

        [DisplayName("Jenis")]
        [Required(ErrorMessage = "Jenis is required.")]
        public string Jenis { get; set; }

        [DisplayName("Kabupaten")]
        [Required(ErrorMessage = "Kabupaten is required.")]
        public string Kabupaten { get; set; }
        [DisplayName("Propinsi")]
        [Required(ErrorMessage = "Propinsi is required.")]
        public string Propinsi { get; set; }

    }
}
