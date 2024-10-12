using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06_BUS
{
    public class AvatarService
    {
        public void AddAvatar(Avatar avatar)
        {
            using (StudentContextDB context = new StudentContextDB())
            {
                context.Avatars.Add(avatar);
                context.SaveChanges();
            }
        }

        public void UpdateAvatar(Avatar avatar)
        {
            using (StudentContextDB context = new StudentContextDB())
            {
                context.Entry(avatar).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Avatar GetAvatarByName(string avatarName)
        {
            using (StudentContextDB context = new StudentContextDB())
            {
                return context.Avatars.SingleOrDefault(a => a.Avatar1 == avatarName);
            }
        }

        public List<Avatar> GetAllAvatars()
        {
            using (StudentContextDB context = new StudentContextDB())
            {
                return context.Avatars.ToList();
            }
        }
    }
}
