using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm100.Models
{
   public interface IChat
    {
        void Join(Agent agent);
        bool Start();
        bool Stop();
        bool Send(User from, User to);
        bool Receive(User from, User to);
    }
}
