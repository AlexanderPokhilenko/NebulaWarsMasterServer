using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class WarshipsCharacteristicsService
    {
        private readonly Dictionary<WarshipTypeEnum, WarshipCharacteristics> warshipParameters =
            new Dictionary<WarshipTypeEnum, WarshipCharacteristics>
            {
                {
                    WarshipTypeEnum.Hare, new WarshipCharacteristics
                    {
                        DefenceParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Health",
                                BaseValue = 1500,
                                Increment = IncrementCoefficient.HealthPoints,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Movement speed",
                                BaseValue = 3,
                                Increment = IncrementCoefficient.LinearVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Rotation speed",
                                BaseValue = 135,
                                Increment = IncrementCoefficient.AngularVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Mass",
                                BaseValue = 100,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        AttackName = "Machine gun and plasma",
                        AttackDescription = "Two twin machine guns and slow powerful plasma gun cause great damage to enemies.",
                        AttackParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Shoot damage (hit) x2",
                                BaseValue = 50,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Shoot cooldown (sec)",
                                BaseValue = 0.125f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Plasma damage (hit)",
                                BaseValue = 150,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Plasma cooldown (sec)",
                                BaseValue = 1.5f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        UltimateName = "Energy ball",
                        UltimateDescription = "Huge slow energy ball ignores any obstacles and shields, causing enormous damage.",
                        UltimateParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Cooldown (sec)",
                                BaseValue = 10,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Damage (per second)",
                                BaseValue = 750,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Lifetime (sec)",
                                BaseValue = 5,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Speed (relative)",
                                BaseValue = 3,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        }
                    }
                },
                {
                    WarshipTypeEnum.Bird, new WarshipCharacteristics
                    {
                        DefenceParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Health",
                                BaseValue = 1750,
                                Increment = IncrementCoefficient.HealthPoints,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Movement speed",
                                BaseValue = 4,
                                Increment = IncrementCoefficient.LinearVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Rotation speed",
                                BaseValue = 180,
                                Increment = IncrementCoefficient.AngularVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Mass",
                                BaseValue = 100,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        AttackName = "Plasma and laser",
                        AttackDescription = "Two triple plasma guns cause huge damage to enemies. Weak laser ignores shields and obstacles.",
                        AttackParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Plasma damage (hit) x2",
                                BaseValue = 100,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Plasma cooldown (sec)",
                                BaseValue = 0.25f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Laser damage (per second)",
                                BaseValue = 75,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Laser cooldown (sec)",
                                BaseValue = 1f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        UltimateName = "Incinerating beam",
                        UltimateDescription = "Overloaded laser creates powerful beam that ignores obstacles and shields, incinerating enemies.",
                        UltimateParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Cooldown (sec)",
                                BaseValue = 10,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Damage (per second)",
                                BaseValue = 750,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Lifetime (sec)",
                                BaseValue = 2,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        }
                    }
                },
                {
                    WarshipTypeEnum.Smiley, new WarshipCharacteristics
                    {
                        DefenceParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Health",
                                BaseValue = 2000,
                                Increment = IncrementCoefficient.HealthPoints,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Movement speed",
                                BaseValue = 2.75f,
                                Increment = IncrementCoefficient.LinearVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Rotation speed",
                                BaseValue = 135,
                                Increment = IncrementCoefficient.AngularVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Mass",
                                BaseValue = 150,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        AttackName = "Synchronous machine guns",
                        AttackDescription = "Four twin machine guns use sequential shooting and cause significant damage to enemies. Of the eight guns, only 4 fire at the same time.",
                        AttackParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Shoot damage (hit) x4",
                                BaseValue = 50,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Shoot cooldown (sec)",
                                BaseValue = 0.25f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        UltimateName = "Spider missile",
                        UltimateDescription = "The torpedo follows the target, turning into a spider bot on impact. Both objects can be shot down.",
                        UltimateParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Cooldown (sec)",
                                BaseValue = 10,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Missile damage (hit)",
                                BaseValue = 200,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Both lifetime (sec)",
                                BaseValue = 10,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Bot damage (per second)",
                                BaseValue = 100,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            }
                        }
                    }
                },
                {
                    WarshipTypeEnum.Sage, new WarshipCharacteristics
                    {
                        DefenceParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Health",
                                BaseValue = 1450,
                                Increment = IncrementCoefficient.HealthPoints,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Movement speed",
                                BaseValue = 5,
                                Increment = IncrementCoefficient.LinearVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Rotation speed",
                                BaseValue = 270,
                                Increment = IncrementCoefficient.AngularVelocity,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Mass",
                                BaseValue = 90,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        AttackName = "Blasters",
                        AttackDescription = "This ship has only one twin blaster. The minor main damage is compensated by the strength of his interceptors.",
                        AttackParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Blaster damage (hit)",
                                BaseValue = 75,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            },
                            new WarshipParameter
                            {
                                Name = "Blaster cooldown (sec)",
                                BaseValue = 0.125f,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            }
                        },
                        UltimateName = "Interceptor",
                        UltimateDescription = "Warship summons fast interceptor drone. It is very powerful, but has low health points.",
                        UltimateParameters = new[]
                        {
                            new WarshipParameter
                            {
                                Name = "Cooldown (sec)",
                                BaseValue = 5,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Lifetime (sec)",
                                BaseValue = 10,
                                Increment = IncrementCoefficient.None,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.None
                            },
                            new WarshipParameter
                            {
                                Name = "Average damage (per second)",
                                BaseValue = 500,
                                Increment = IncrementCoefficient.Attack,
                                UiIncrementTypeEnum = UiIncrementTypeEnum.Plus
                            }
                        }
                    }
                }
            };
        
        public WarshipCharacteristics GetWarshipCharacteristics(WarshipTypeEnum warshipTypeEnum)
        {
            if (warshipParameters.TryGetValue(warshipTypeEnum, out var result))
            {
                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(warshipTypeEnum));
            }
        }
    }
}