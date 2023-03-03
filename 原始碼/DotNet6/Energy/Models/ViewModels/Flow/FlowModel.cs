using System.ComponentModel.DataAnnotations;

namespace Energy.Models.ViewModels.Flow
{
    public class FlowModel
    {
        [Display(Name = "編號")]
        [Required(ErrorMessage = "必填")]
        public string Id { get; set; }

        [Display(Name = "頁面名稱")]
        public string Pagename { get; set; }

        [Display(Name = "父層編號")]
        public string Parentid { get; set; }

        [Display(Name = "深度")]
        [Required(ErrorMessage = "必填")]
        [RegularExpression("\\d+", ErrorMessage = "格式錯誤")]
        public int Depth { get; set; }

        [Display(Name = "名稱")]
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; }

        [Display(Name = "產業別編號")]
        [Required(ErrorMessage = "必填")]
        public string RowNo1 { get; set; }

        [Display(Name = "欄位編號")]
        public string Colidlist { get; set; }

        [Display(Name = "小數點位數")]
        public int DecimalPlaces { get; set; }

        public string[] ColdIdAry
        {
            get
            {
                if (!string.IsNullOrEmpty(Colidlist))
                {
                    return Colidlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return new string[3];
                }
            }
        }

        public int Iorder { get; set; }

        public int[] RowNo1Ary
        {
            get
            {
                if (!string.IsNullOrEmpty(RowNo1))
                {
                    var temp = RowNo1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> returnList = new List<int>();
                    for (int i = 0; i < temp.Length; i++)
                    {
                        int iii = 0;
                        if (int.TryParse(temp[i], out iii))
                        {
                            returnList.Add(iii);
                        }
                    }
                    return returnList.ToArray<int>();
                }
                else
                {
                    return new int[0];
                }
            }
        }
    }
}
