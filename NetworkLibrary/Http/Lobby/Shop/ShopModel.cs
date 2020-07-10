﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
  using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Хранит в себе всю информацию про раздылы в магазине.
    /// </summary>
    [ZeroFormattable]
    public class ShopModel
    {
        [Index(0)] public virtual List<SectionModel> UiSections { get; set; }
    }
}
