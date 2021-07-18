using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UNCDF.Layers.Model;

namespace UNCDF.CMS.Models
{
    public class BannerViewModel
    {
    [Required]
    public int BannerId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string SubTile { get; set; }

    [Required]
    public string Image { get; set; }
    public string ImageLink { get; set; }


    public int Status { get; set; }

    public string ImageExtension { get; set; }
    public byte[] FileByte { get; set; }

}

public class SearchBannerViewModel
{
    public string Title { get; set; }
    public int Status { get; set; }

    [Display(Name = "")]
    public ResultSearchBannerViewModel Result { get; set; }
}

public class ResultSearchBannerViewModel
{
    public int BannerId { get; set; }
    public string Title { get; set; }

    [Display(Name = "Status")]
    public string StatusName { get; set; }

    public int Status { get; set; }
}
}