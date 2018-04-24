using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    public class LoginUserContext
    {
        /// <summary>
        /// 返回一个登录的用户信息
        /// </summary>
        /// <returns></returns>
        //public static View_Login_User GetLoginUser()
        //{
        //    if (HttpContext.Current.Session["LoginUserContext"] != null)
        //        return HttpContext.Current.Session["LoginUserContext"] as View_Login_User;
        //    else
        //        throw new ExceptionUserNull("用户已过期，请重新登录");
        //}

        /// <summary>
        /// 返回一个登录的企业信息 (外网)
        /// </summary>
        /// <returns></returns>
        //public static View_comp_t_regist GetOuterLoginUserInfo()
        //{
        //    if (HttpContext.Current.Session["OuterLoginUserInfo"] != null)
        //        return HttpContext.Current.Session["OuterLoginUserInfo"] as View_comp_t_regist;
        //    else
        //        return null;
        //}
        /// <summary>
        /// 将User信息存到Session里
        /// </summary>
        /// <returns></returns>
        //public static void SetLoginUser(View_Login_User user)
        //{
        //    HttpContext.Current.Session["LoginUserContext"] = user;
        //}

        /// <summary>
        /// 清空User信息
        /// </summary>
        /// <returns></returns>
        //public static void ClearLoginUser()
        //{
        //    HttpContext.Current.Session.Remove("LoginUserContext");
        //}


        /// <summary>
        /// 存放菜单ID
        /// </summary>
        /// <param name="MenuId"></param>
        //public static void SetSelectedMenuId(string menuId)
        //{
        //    HttpContext.Current.Session["SelectedMenuID"] = menuId;
        //}
        /// <summary>
        /// 获取菜单ID
        /// </summary>
        /// <returns></returns>
        //public static string GetSelectedMenuID()
        //{
        //    if (HttpContext.Current.Session["SelectedMenuID"] != null)
        //    {
        //        return HttpContext.Current.Session["SelectedMenuID"].ToString();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
