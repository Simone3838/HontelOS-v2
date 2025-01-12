/*
* PROJECT:          HontelOS
* CONTENT:          System control
* PROGRAMMERS:      Jort van Dalen
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HontelOS.System.Graphics
{
    public interface SystemControl
    {
        public void Draw();
        public void Update();
    }
}
