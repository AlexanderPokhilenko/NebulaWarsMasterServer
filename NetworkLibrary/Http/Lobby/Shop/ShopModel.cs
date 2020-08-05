﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
  using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Хранит в себе всю информацию про раздылы в магазине.
    /// </summary>
    [ZeroFormattable]
    public class ShopModel
    {
        [Index(0)] public virtual int Id { get; set; }
        [Index(1)] public virtual List<SectionModel> UiSections { get; set; }
        
        public Dictionary<SectionTypeEnum, string>  GetRequiredSectionNames()
        {
            var dict  = new Dictionary<SectionTypeEnum, string>();
            foreach (SectionModel sectionModel in UiSections)
            {
                if (sectionModel.SectionTypeEnum != null)
                {
                    dict.Add(sectionModel.SectionTypeEnum.Value, sectionModel.HeaderName);
                }
            }

            return dict;
        } 
    }
}
