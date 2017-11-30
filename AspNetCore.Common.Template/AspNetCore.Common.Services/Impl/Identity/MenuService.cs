using AspNetCore.Common.Infrastructure.Interface;
using AspNetCore.Common.Models.Identity;
using AspNetCore.Common.Models.Identity.ViewModel;
using AspNetCore.Common.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Common.Services.Identity.Impl
{
    public class MenuService : IMenu
    {
        #region variables

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbSet<Menu> _menuRepository;

        #endregion variables

        #region ctor

        public MenuService(IIdentityDbContext unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _menuRepository = _unitOfWork.Set<Menu>();
        }

        #endregion ctor

        #region public methods
        public void AddMenu(Menu model)
        {
            var id = 0;
            model.IsLeaf = true;
            if (model.ParentId == null)
            {
                model.ParentId = 0;
                model.IsLeaf = false;
            }
            id = _menuRepository.Where(t => t.ParentId == model.ParentId).Max(t => t.Id);
            if (id == 0)
            {
                model.Id = (int)model.ParentId * 100 + 1;
            }
            else
            {
                model.Id = id + 1;
            }

            _menuRepository.Add(model);
            _unitOfWork.Commit();
        }

        public void DeleteMenu(int id)
        {
            var menu = _menuRepository.Find(id);
            _menuRepository.Remove(menu);
            _unitOfWork.Commit();
        }
        public void EditMenu(Menu model)
        {
            _menuRepository.Update(model);
            _unitOfWork.Commit();
        }

        public Task<Menu> GetMenuByIdAsync(int menuId)
        {
            return _menuRepository.FindAsync(menuId);
        }

        public Menu GetMenuById(int menuId)
        {
            return _menuRepository.Find(menuId);
        }

        public List<Menu> GetSecRootById(int id)
        {
            return _menuRepository.Where(x => x.ParentId == id).OrderBy(t => t.Sequence).ToList();
        }

        public Task<Menu> GetMenuByNameAsync(string name)
        {
            return _menuRepository.FirstOrDefaultAsync(x => x.Name == name);
        }

        public Task<MenuViewModel> GetRootMenusByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<MenuViewModel> GetAllRootMenus()
        {
            var menus = _menuRepository;
            var result = BuildTree(CastToViewModel(menus.ToList()));
            return Task.FromResult(result);
        }

        //public MenuViewModel CastMenuIdsToMenuModel(IEnumerable<string> menuIds)
        //{
        //    if (menuIds == null || menuIds.Count() == 0)
        //        return new MenuViewModel();

        //    var menus = _menuRepository
        //      .Where(x => menuIds.Contains(x.Id.ToString()))
        //      .ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider).ToList();

        //    return BuildTree(menus);
        //}

        //public MenuViewModel CastPResourceToMenu(ICollection<PResource> resource)
        //{
        //    var menuViewModels =
        //        resource.Select(
        //            menu =>
        //                new MenuViewModel
        //                {
        //                    Id = menu.id,
        //                    ParentId = int.Parse(menu.parentId),
        //                    Icon = menu.iconPath,
        //                    Name = menu.name,
        //                    Value = menu.url
        //                });
        //    return BuildTree(menuViewModels);
        //}
        #endregion public methods

        #region private methods

        private IEnumerable<MenuViewModel> CastToViewModel(List<Menu> menus)
        {
            return
                menus.Select(
                    menu =>
                        new MenuViewModel
                        {
                            Id = menu.Id,
                            ParentId = menu.ParentId,
                            Icon = menu.Icon,
                            Sequence = menu.Sequence,
                            Name = menu.Name,
                            Value = menu.Value,
                        });
        }

        #region 递归查找父节点

        //算法有点不好理解，多看一哈
        private MenuViewModel BuildTree(IEnumerable<MenuViewModel> source)
        {
            var groups = source.GroupBy(i => i.ParentId);

            var root = groups.FirstOrDefault(g => g.Key.HasValue == false).First();
            //var root = new MenuViewModel() { Id = 0, ParentId = null, Name = "Root" };

            var dict = groups.Where(x => x.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.OrderBy(x => x.Sequence).ToList());

            AddChildren(root, dict);

            return root;
        }

        private List<int> GetParentIds(int[] ids)
        {
            var allIds = new List<int>();
            foreach (var id in ids)
            {
                allIds.AddRange(SplitId(id));
            }
            return allIds;
        }

        private List<int> SplitId(int id)
        {
            var idStr = id.ToString();
            var result = new List<int>();
            for (var i = 2; i <= idStr.Length; i += 2)
            {
                result.Add(int.Parse(idStr.Substring(0, i)));
            }
            return result;
        }

        private void RemoveParent(IDictionary<int, List<MenuViewModel>> source, List<int> parentIds)
        {
            for (var i = 0; i < parentIds.Count; i++)
            {
                if (!source.ContainsKey(parentIds[i]))
                {
                    source.Remove(parentIds[i]);
                }
            }
        }

        private void AddChildren(MenuViewModel node, IDictionary<int, List<MenuViewModel>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.ClildMenu = source[node.Id].OrderBy(x => x.Sequence).ToList();
                for (var i = 0; i < node.ClildMenu.Count; i++)
                    AddChildren(node.ClildMenu[i], source);
            }
            else
            {
                node.ClildMenu = new List<MenuViewModel>();
            }
        }

        #endregion 递归查找父节点

        #endregion
    }
}