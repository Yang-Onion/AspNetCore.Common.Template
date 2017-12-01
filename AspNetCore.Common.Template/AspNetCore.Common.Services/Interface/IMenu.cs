using AspNetCore.Common.Models.Identity;
using AspNetCore.Common.Models.Identity.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Common.Services.Interface
{
    public interface IMenu
    {
        void AddMenu(Menu menu);
        void DeleteMenu(int menuId);
        void EditMenu(Menu menu);
        Menu GetMenuById(int menuId);
        Task<Menu> GetMenuByIdAsync(int menuId);
        Task<Menu> GetMenuByNameAsync(string name);
        Task<MenuViewModel> GetRootMenusByRoleId(int roleId);

        MenuViewModel CastMenuIdsToMenuModel(IEnumerable<string> menuIds);
    }
}
