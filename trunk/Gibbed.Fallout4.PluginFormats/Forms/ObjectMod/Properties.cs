/* Copyright (c) 2015 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

namespace Gibbed.Fallout4.PluginFormats.Forms.ObjectMod
{
    public static class Properties
    {
        public enum ArmorFormProperty : ushort
        {
            Enchantments = 0,
            BashImpactDataSet = 1,
            BlockMaterial = 2,
            Keywords = 3,
            Weight = 4,
            Value = 5,
            Rating = 6,
            AddonIndex = 7,
            BodyPart = 8,
            DamageTypeValues = 9,
            ActorValues = 10,
            Health = 11,
            ColorRemappingIndex = 12,
            MaterialSwaps = 13,
        }

        public static class ArmorForm
        {
            public const ushort Enchantments = (ushort)ArmorFormProperty.Enchantments;
            public const ushort BashImpactDataSet = (ushort)ArmorFormProperty.BashImpactDataSet;
            public const ushort BlockMaterial = (ushort)ArmorFormProperty.BlockMaterial;
            public const ushort Keywords = (ushort)ArmorFormProperty.Keywords;
            public const ushort Weight = (ushort)ArmorFormProperty.Weight;
            public const ushort Value = (ushort)ArmorFormProperty.Value;
            public const ushort Rating = (ushort)ArmorFormProperty.Rating;
            public const ushort AddonIndex = (ushort)ArmorFormProperty.AddonIndex;
            public const ushort BodyPart = (ushort)ArmorFormProperty.BodyPart;
            public const ushort DamageTypeValues = (ushort)ArmorFormProperty.DamageTypeValues;
            public const ushort ActorValues = (ushort)ArmorFormProperty.ActorValues;
            public const ushort Health = (ushort)ArmorFormProperty.Health;
            public const ushort ColorRemappingIndex = (ushort)ArmorFormProperty.ColorRemappingIndex;
            public const ushort MaterialSwaps = (ushort)ArmorFormProperty.MaterialSwaps;
        }

        public enum NPCFormProperty : ushort
        {
            Keywords = 0,
            ForcedInventory = 1,
            XPOffset = 2,
            Enchantments = 3,
            ColorRemappingIndex = 4,
            MaterialSwaps = 5,
        }

        public static class NPCForm
        {
            public const ushort Keywords = (ushort)NPCFormProperty.Keywords;
            public const ushort ForcedInventory = (ushort)NPCFormProperty.ForcedInventory;
            public const ushort XPOffset = (ushort)NPCFormProperty.XPOffset;
            public const ushort Enchantments = (ushort)NPCFormProperty.Enchantments;
            public const ushort ColorRemappingIndex = (ushort)NPCFormProperty.ColorRemappingIndex;
            public const ushort MaterialSwaps = (ushort)NPCFormProperty.MaterialSwaps;
        }

        public enum WeaponFormProperty
        {
            Speed = 0,
            Reach = 1,
            MinRange = 2,
            MaxRange = 3,
            AttackDelaySec = 4,
            //5,
            OutOfRangeDamageMult = 6,
            SecondaryDamage = 7,
            CriticalChargeBonus = 8,
            HitBehavior = 9,
            Rank = 10,
            //11,
            AmmoCapacity = 12,
            //13,
            //14,
            Type = 15,
            IsPlayerOnly = 16,
            // ReSharper disable InconsistentNaming
            NPCsUseAmmo = 17,
            // ReSharper restore InconsistentNaming
            HasChargingReload = 18,
            IsMinorCrime = 19,
            IsFixedRange = 20,
            HasEffectOnDeath = 21,
            HasAlternateRumble = 22,
            IsNonHostile = 23,
            IgnoreResist = 24,
            IsAutomatic = 25,
            CantDrop = 26,
            IsNonPlayable = 27,
            AttackDamage = 28,
            Value = 29,
            Weight = 30,
            Keywords = 31,
            AimModel = 32,
            AimModelMinConeDegrees = 33,
            AimModelMaxConeDegrees = 34,
            AimModelConeIncreasePerShot = 35,
            AimModelConeDecreasePerSec = 36,
            AimModelConeDecreaseDelayMs = 37,
            AimModelConeSneakMultiplier = 38,
            AimModelRecoilDiminishSpringForce = 39,
            AimModelRecoilDiminishSightsMult = 40,
            AimModelRecoilMaxDegPerShot = 41,
            AimModelRecoilMinDegPerShot = 42,
            AimModelRecoilHipMult = 43,
            AimModelRecoilShotsForRunaway = 44,
            AimModelRecoilArcDeg = 45,
            AimModelRecoilArcRotateDeg = 46,
            AimModelConeIronSightsMultiplier = 47,
            HasScope = 48,
            ZoomDataFOVMult = 49,
            FireSeconds = 50,
            NumProjectiles = 51,
            AttackSound = 52,
            AttackSound2D = 53,
            AttackLoop = 54,
            AttackFailSound = 55,
            IdleSound = 56,
            EquipSound = 57,
            UnEquipSound = 58,
            SoundLevel = 59,
            ImpactDataSet = 60,
            Ammo = 61,
            CritEffect = 62,
            BashImpactDataSet = 63,
            BlockMaterial = 64,
            Enchantments = 65,
            AimModelBaseStability = 66,
            ZoomData = 67,
            ZoomDataOverlay = 68,
            ZoomDataImageSpace = 69,
            ZoomDataCameraOffsetX = 70,
            ZoomDataCameraOffsetY = 71,
            ZoomDataCameraOffsetZ = 72,
            EquipSlot = 73,
            SoundLevelMult = 74,
            NPCAmmoList = 75,
            ReloadSpeed = 76,
            DamageTypeValues = 77,
            AccuracyBonus = 78,
            AttackActionPointCost = 79,
            OverrideProjectile = 80,
            HasBoltAction = 81,
            StaggerValue = 82,
            SightedTransitionSeconds = 83,
            FullPowerSeconds = 84,
            HoldInputToPower = 85,
            HasRepeatableSingleFire = 86,
            MinPowerPerShot = 87,
            ColorRemappingIndex = 88,
            MaterialSwaps = 89,
            CriticalDamageMult = 90,
            FastEquipSound = 91,
            DisableShells = 92,
            HasChargingAttack = 93,
            ActorValues = 94,
        }

        public static class WeaponForm
        {
            public const ushort Speed = (ushort)WeaponFormProperty.Speed;
            public const ushort Reach = (ushort)WeaponFormProperty.Reach;
            public const ushort MinRange = (ushort)WeaponFormProperty.MinRange;
            public const ushort MaxRange = (ushort)WeaponFormProperty.MaxRange;
            public const ushort AttackDelaySec = (ushort)WeaponFormProperty.AttackDelaySec;

            public const ushort OutOfRangeDamageMult = (ushort)WeaponFormProperty.OutOfRangeDamageMult;
            public const ushort SecondaryDamage = (ushort)WeaponFormProperty.SecondaryDamage;
            public const ushort CriticalChargeBonus = (ushort)WeaponFormProperty.CriticalChargeBonus;
            public const ushort HitBehavior = (ushort)WeaponFormProperty.HitBehavior;
            public const ushort Rank = (ushort)WeaponFormProperty.Rank;

            public const ushort AmmoCapacity = (ushort)WeaponFormProperty.AmmoCapacity;

            public const ushort Type = (ushort)WeaponFormProperty.Type;
            public const ushort IsPlayerOnly = (ushort)WeaponFormProperty.IsPlayerOnly;
            // ReSharper disable InconsistentNaming
            public const ushort NPCsUseAmmo = (ushort)WeaponFormProperty.NPCsUseAmmo;
            // ReSharper restore InconsistentNaming
            public const ushort HasChargingReload = (ushort)WeaponFormProperty.HasChargingReload;
            public const ushort IsMinorCrime = (ushort)WeaponFormProperty.IsMinorCrime;
            public const ushort IsFixedRange = (ushort)WeaponFormProperty.IsFixedRange;
            public const ushort HasEffectOnDeath = (ushort)WeaponFormProperty.HasEffectOnDeath;
            public const ushort HasAlternateRumble = (ushort)WeaponFormProperty.HasAlternateRumble;
            public const ushort IsNonHostile = (ushort)WeaponFormProperty.IsNonHostile;
            public const ushort IgnoreResist = (ushort)WeaponFormProperty.IgnoreResist;
            public const ushort IsAautomatic = (ushort)WeaponFormProperty.IsAutomatic;
            public const ushort CantDrop = (ushort)WeaponFormProperty.CantDrop;
            public const ushort IsNonPlayable = (ushort)WeaponFormProperty.IsNonPlayable;
            public const ushort AttackDamage = (ushort)WeaponFormProperty.AttackDamage;
            public const ushort Value = (ushort)WeaponFormProperty.Value;
            public const ushort Weight = (ushort)WeaponFormProperty.Weight;
            public const ushort Keywords = (ushort)WeaponFormProperty.Keywords;
            public const ushort AimModel = (ushort)WeaponFormProperty.AimModel;
            public const ushort AimModelMinConeDegrees = (ushort)WeaponFormProperty.AimModelMinConeDegrees;
            public const ushort AimModelMaxConeDegrees = (ushort)WeaponFormProperty.AimModelMaxConeDegrees;
            public const ushort AimModelConeIncreasePerShot = (ushort)WeaponFormProperty.AimModelConeIncreasePerShot;
            public const ushort AimModelConeDecreasePerSec = (ushort)WeaponFormProperty.AimModelConeDecreasePerSec;
            public const ushort AimModelConeDecreaseDelayMs = (ushort)WeaponFormProperty.AimModelConeDecreaseDelayMs;
            public const ushort AimModelConeSneakMultiplier = (ushort)WeaponFormProperty.AimModelConeSneakMultiplier;

            public const ushort AimModelRecoilDiminishSpringForce =
                (ushort)WeaponFormProperty.AimModelRecoilDiminishSpringForce;

            public const ushort AimModelRecoilDiminishSightsMult =
                (ushort)WeaponFormProperty.AimModelRecoilDiminishSightsMult;

            public const ushort AimModelRecoilMaxDegPerShot = (ushort)WeaponFormProperty.AimModelRecoilMaxDegPerShot;
            public const ushort AimModelRecoilMinDegPerShot = (ushort)WeaponFormProperty.AimModelRecoilMinDegPerShot;
            public const ushort AimModelRecoilHipMult = (ushort)WeaponFormProperty.AimModelRecoilHipMult;
            public const ushort AimModelRecoilShotsForRunaway = (ushort)WeaponFormProperty.AimModelRecoilShotsForRunaway;
            public const ushort AimModelRecoilArcDeg = (ushort)WeaponFormProperty.AimModelRecoilArcDeg;
            public const ushort AimModelRecoilArcRotateDeg = (ushort)WeaponFormProperty.AimModelRecoilArcRotateDeg;

            public const ushort AimModelConeIronSightsMultiplier =
                (ushort)WeaponFormProperty.AimModelConeIronSightsMultiplier;

            public const ushort HasScope = (ushort)WeaponFormProperty.HasScope;
            public const ushort ZoomDataFOVMult = (ushort)WeaponFormProperty.ZoomDataFOVMult;
            public const ushort FireSeconds = (ushort)WeaponFormProperty.FireSeconds;
            public const ushort NumProjectiles = (ushort)WeaponFormProperty.NumProjectiles;
            public const ushort AttackSound = (ushort)WeaponFormProperty.AttackSound;
            public const ushort AttackSound2D = (ushort)WeaponFormProperty.AttackSound2D;
            public const ushort AttackLoop = (ushort)WeaponFormProperty.AttackLoop;
            public const ushort AttackFailSound = (ushort)WeaponFormProperty.AttackFailSound;
            public const ushort IdleSound = (ushort)WeaponFormProperty.IdleSound;
            public const ushort EquipSound = (ushort)WeaponFormProperty.EquipSound;
            public const ushort UnEquipSound = (ushort)WeaponFormProperty.UnEquipSound;
            public const ushort SoundLevel = (ushort)WeaponFormProperty.SoundLevel;
            public const ushort ImpactDataSet = (ushort)WeaponFormProperty.ImpactDataSet;
            public const ushort Ammo = (ushort)WeaponFormProperty.Ammo;
            public const ushort CritEffect = (ushort)WeaponFormProperty.CritEffect;
            public const ushort BashImpactDataSet = (ushort)WeaponFormProperty.BashImpactDataSet;
            public const ushort BlockMaterial = (ushort)WeaponFormProperty.BlockMaterial;
            public const ushort Enchantments = (ushort)WeaponFormProperty.Enchantments;
            public const ushort AimModelBaseStability = (ushort)WeaponFormProperty.AimModelBaseStability;
            public const ushort ZoomData = (ushort)WeaponFormProperty.ZoomData;
            public const ushort ZoomDataOverlay = (ushort)WeaponFormProperty.ZoomDataOverlay;
            public const ushort ZoomDataImageSpace = (ushort)WeaponFormProperty.ZoomDataImageSpace;
            public const ushort ZoomDataCameraOffsetX = (ushort)WeaponFormProperty.ZoomDataCameraOffsetX;
            public const ushort ZoomDataCameraOffsetY = (ushort)WeaponFormProperty.ZoomDataCameraOffsetY;
            public const ushort ZoomDataCameraOffsetZ = (ushort)WeaponFormProperty.ZoomDataCameraOffsetZ;
            public const ushort EquipSlot = (ushort)WeaponFormProperty.EquipSlot;
            public const ushort SoundLevelMult = (ushort)WeaponFormProperty.SoundLevelMult;
            public const ushort NPCAmmoList = (ushort)WeaponFormProperty.NPCAmmoList;
            public const ushort ReloadSpeed = (ushort)WeaponFormProperty.ReloadSpeed;
            public const ushort DamageTypeValues = (ushort)WeaponFormProperty.DamageTypeValues;
            public const ushort AccuracyBonus = (ushort)WeaponFormProperty.AccuracyBonus;
            public const ushort AttackActionPointCost = (ushort)WeaponFormProperty.AttackActionPointCost;
            public const ushort OverrideProjectile = (ushort)WeaponFormProperty.OverrideProjectile;
            public const ushort BoltAction = (ushort)WeaponFormProperty.HasBoltAction;
            public const ushort StaggerValue = (ushort)WeaponFormProperty.StaggerValue;
            public const ushort SightedTransitionSeconds = (ushort)WeaponFormProperty.SightedTransitionSeconds;
            public const ushort FullPowerSeconds = (ushort)WeaponFormProperty.FullPowerSeconds;
            public const ushort HoldInputToPower = (ushort)WeaponFormProperty.HoldInputToPower;
            public const ushort HasRepeatableSingleFire = (ushort)WeaponFormProperty.HasRepeatableSingleFire;
            public const ushort MinPowerPerShot = (ushort)WeaponFormProperty.MinPowerPerShot;
            public const ushort ColorRemappingIndex = (ushort)WeaponFormProperty.ColorRemappingIndex;
            public const ushort MaterialSwaps = (ushort)WeaponFormProperty.MaterialSwaps;
            public const ushort CriticalDamageMult = (ushort)WeaponFormProperty.CriticalDamageMult;
            public const ushort FastEquipSound = (ushort)WeaponFormProperty.FastEquipSound;
            public const ushort DisableShells = (ushort)WeaponFormProperty.DisableShells;
            public const ushort HasChargingAttack = (ushort)WeaponFormProperty.HasChargingAttack;
            public const ushort ActorValues = (ushort)WeaponFormProperty.ActorValues;
        }
    }
}
