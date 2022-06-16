using System.Collections.Generic;

namespace web.ui.viewmodel
{
    public class Test
    {
        public string name { get; set; }
        public string address { get; set; }
        public List<TestItems> lstFriends { get; set; }
    }

    public class TestItems 
    {
        public string friends { get; set; }
    }
}