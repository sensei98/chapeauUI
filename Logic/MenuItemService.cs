using System;
using System.Collections.Generic;
using DAL;
using Model;

namespace Logic
{
    public class MenuItemService
    {
        private MenuItemDAO menuItemDAO = new MenuItemDAO();
        public List<MenuItem> GetAllMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            try
            {
                menuItems = menuItemDAO.GetAllMenuItems();
                return menuItems;
            }
            catch (Exception)
            {
                MenuItem item = new MenuItem()
                {
                    ProductID = 1,
                    Name = "menu",
                    Category = Category.unspecified,
                    Price = 1,
                    PreparationTime = 1
                };
                menuItems.Add(item);
                return menuItems;
            }
        }
        public List<MenuItem> GetSubMenu(string submenu)
        {
            try
            {
                Category cat = (Category)Enum.Parse(typeof(Category), submenu);
                List<MenuItem> items = menuItemDAO.GetSubMenu(cat);
                return items;
            }

            catch (Exception)
            {
                return new List<MenuItem>();
            }
        }
        
        public MenuItem GetItemForID(int id)
        {
            try
            {
                return menuItemDAO.GetItemForID(id);
            }

            catch
            {
                return new MenuItem();
            }
        }
        public int EditMenuItem(MenuItem item)
        {
            int items;
            try
            {
                return items = menuItemDAO.EditMenuItem(item);
            }
            catch (Exception)
            {
                return items = 0;
            }
        }
        public int AddStockAmount(MenuItem item)
        {
            int items;
            try
            {
                return items = menuItemDAO.AddStockAmount(item);
            }
            catch (Exception)
            {
                return items = 0;
            }
        }
        public int RemoveMenuItem(MenuItem item)
        {
            int items;
            try
            {
                items = menuItemDAO.RemoveMenuItem(item);
                return items;
            }
            catch (Exception)
            {
                return items = 0;
            }
        }
        public int AddMenuItem(MenuItem item)
        {
            int items;
            try
            {
                return items = menuItemDAO.AddMenuItem(item);
            }
            catch (Exception)
            {
                return items = 0;
            }
        }
    }
}
