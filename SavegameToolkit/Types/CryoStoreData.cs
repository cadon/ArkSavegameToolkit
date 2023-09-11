using System;
using System.Collections.Generic;
using System.Text;

namespace SavegameToolkit.Types
{
    public class CryoStoreData
    {

        public GameObject ParentObject { get; set; }
        public int StoreDataIndex { get; set; } = 1;
        public long Offset { get; set; }
        public byte[] Data { get; set; }

    }
}
