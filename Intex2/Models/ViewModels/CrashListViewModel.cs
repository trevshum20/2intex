using System.Collections.Generic;
using Intex2.Models;
namespace Intex2.Models.ViewModels
{
    public class CrashListViewModel
    {
        public IEnumerable<utah_crashes_table> Crashes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public Search Search { get; set; }
    }
}
