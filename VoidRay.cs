using MelonLoader;
using static SC2Expansion.ModMain;
using SC2Expansion;
using MelonLoader.Utils;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Towers.Upgrades;
using Il2CppInterop.Runtime;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using UnityEngine.Playables;
using System.Collections;
using Il2CppInterop.Runtime.Attributes;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using UnityEngine.InputSystem;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Il2CppAssets.Scripts.Unity.Effects;
[assembly:MelonGame("Ninja Kiwi","BloonsTD6")]
[assembly:MelonInfo(typeof(VoidRay.SC2ModMain),VoidRay.ModHelperData.Name,VoidRay.ModHelperData.Version,"Silentstorm")]
namespace VoidRay{
    public class SC2ModMain:MelonMod{}
    public class VoidRay:SC2Tower{
        public override string Name=>"VoidRay";
        public override Faction TowerFaction=>Faction.Protoss;
        public override UpgradeModel[]GenerateUpgradeModels(){
            return new UpgradeModel[]{
                new("Prismatic Range",980,0,new("Ui["+Name+"-PrismaticRangeIcon]"),0,0,0,"","Prismatic Range"),
                new("Flux Vanes",1475,0,new("Ui["+Name+"-FluxVanesIcon]"),0,1,0,"","Flux Vanes"),
                new("Prismatic Alignment",2450,0,new("Ui["+Name+"-PrismaticAlignmentIcon]"),0,2,0,"","Prismatic Alignment"),
                new(Name+"Destroyer",8500,0,new("Ui["+Name+"-DestroyerIcon]"),0,3,0,"",Name+"Destroyer"),
                new("Mohandar",14500,0,new("Ui["+Name+"-MohandarIcon]"),0,4,0,"","Mohandar")
            };
            //lightning prefab
            // 90,0,0 rotation
        }
		public override Dictionary<string,Il2CppSystem.Type>Components=>new(){{Name+"-Prefab",Il2CppType.Of<VoidRayCom>()},
            {Name+"-DestroyerPrefab",Il2CppType.Of<VoidRayCom>()},{Name+"-MohandarPrefab",Il2CppType.Of<VoidRayCom>()}};
        public override Dictionary<string,Il2CppSystem.Type>Beams=>new(){{Name+"-Beam",Il2CppType.Of<VoidRayBeam>()},
            {Name+"-DestroyerBeam",Il2CppType.Of<VoidRayDestroyerBeam>()},{Name+"-MohandarBeam",Il2CppType.Of<VoidRayMohandarBeam>()}};
        public override Dictionary<string,Tuple<string,int>>SpritePrefabLayers=>new(){{$"{Name}-BeamBounceSmall",
            new("ProjectilesOverBloons",5)},{$"{Name}-BeamBounceMedium",new("ProjectilesOverBloons",5)},{$"{Name}-BeamBounceLarge",
            new("ProjectilesOverBloons",5)}};
        [RegisterTypeInIl2Cpp]
        public class VoidRayBeam:SC2BeamCom{
            public VoidRayBeam(IntPtr ptr):base(ptr){}
            public override string Name=>Name+"-Beam";
            public override Vector4 DarkColour=>new(0,0.5f,1,1);
            public override Vector4 LightColour=>new(0,0.5f,1,1);
        }
        [RegisterTypeInIl2Cpp]
        public class VoidRayDestroyerBeam:SC2BeamCom{
            public VoidRayDestroyerBeam(IntPtr ptr):base(ptr){}
            public override string Name=>Name+"-DestroyerBeam";
            public override Vector4 DarkColour=>new(0.45f,0,0,1);
            public override Vector4 LightColour=>new(0.75f,0,0,1);
            public override Vector4 Colour=>new(0,0,0,1);
        }
        [RegisterTypeInIl2Cpp]
        public class VoidRayMohandarBeam:SC2BeamCom{
            public VoidRayMohandarBeam(IntPtr ptr):base(ptr){}
            public override string Name=>Name+"-MohandarBeam";
            public override Vector4 DarkColour=>new(1,0.501f,0,1);
            public override Vector4 LightColour=>new(1,0.5196f,0,1);
        }
		[RegisterTypeInIl2Cpp]
        public class VoidRayCom:MonoBehaviour{
            public VoidRayCom(IntPtr ptr):base(ptr){}
            public Animator Anim;
            void Start(){
                Anim=GetComponent<Animator>();
            }
            public float Weight;
            public void Update(){
                Weight=Anim.GetLayerWeight(1);
                if(Weight<1.01f){
                    Anim.SetLayerWeight(1,Weight+0.02f);
                }
            }
        }
        public override int MaxTier=>5;
        public override ShopTowerDetailsModel ShopDetails(){
            ShopTowerDetailsModel details=gameModel.towerSet[0].Clone<ShopTowerDetailsModel>();
            details.towerId=Name;
            details.name=Name;
            details.towerIndex=12;
            details.pathOneMax=5;
            details.pathTwoMax=0;
            details.pathThreeMax=0;
            LocManager.textTable.Add(Name,"Void Ray");
            LocManager.textTable.Add(UpgradeModels[3].name,"Destroyer");
			LocManager.textTable.Add(Name+" Description","Flying Protoss craft, the first example of a Khalai and Nerazim design. Fires a high "
                +"powered laser against bloons");
            LocManager.textTable.Add(UpgradeModels[0].name+" Description","Refining the prismatic core more lets it fire further");
            LocManager.textTable.Add(UpgradeModels[1].name+" Description","Better flux vanes allow for a faster attack");
            LocManager.textTable.Add(UpgradeModels[2].name+" Description","Multiplies damage dealt to MOAB class bloons by 3");
            LocManager.textTable.Add($"{UpgradeModels[3].name} Description","Stolen Void Rays retrofitted with bloodshards instead of a "+
                 "prismatic core. Beam bounces to nearby targets");
            LocManager.textTable.Add(UpgradeModels[4].name+" Description","Predecessor to the current Nerazim leader, Vorazun, deals huge damage against MOAB bloons");
            return details;
        }
        public override TowerModel[]GenerateTowerModels(){
            return new TowerModel[]{
                Base(),
                PrismaticRange(),
                FluxVanes(),
                PrismaticAlignment(),
                Destroyer(),
                Mohandar()
            };
        }
        public TowerModel Base(){
            TowerModel voidRay=gameModel.GetTowerFromId("DartMonkey").Clone().Cast<TowerModel>();
            voidRay.mods=new(0);
            voidRay.name=Name;
            voidRay.baseId=Name;
            voidRay.towerSet=(TowerSet)2;
            voidRay.cost=1500;
            voidRay.tier=0;
            voidRay.tiers=new[]{0,0,0};
            voidRay.upgrades=new UpgradePathModel[]{new(UpgradeModels[0].name,Name+"-100")};
            voidRay.range=50;
            voidRay.display=new(Name+"-Prefab");
            voidRay.icon=new("Ui["+Name+"-Icon]");
            voidRay.instaIcon=voidRay.icon;
            voidRay.portrait=new("Ui["+Name+"-Portrait]");
            DisplayModel display=voidRay.behaviors.GetModel<DisplayModel>();
            display.positionOffset=new(0,0,100);
            display.display=voidRay.display;
            voidRay.areaTypes=FlyingAreaTypes;
            List<Model>voidRayBehav=voidRay.behaviors.ToList();
            voidRayBehav.Add(SelectedSoundModel.Clone<CreateSoundOnSelectedModel>());
            AttackModel voidRayBeam=voidRayBehav.GetModel<AttackModel>();
            voidRayBeam.range=voidRay.range;
            voidRayBeam.offsetZ=100;
            voidRayBeam.weapons[0]=gameModel.GetTowerFromId("BallOfLightTower").behaviors.GetModel<AttackModel>().weapons[0].
                Clone<WeaponModel>();
            WeaponModel voidRayBeamWeapon=voidRayBeam.weapons[0];
            voidRayBeamWeapon.ejectY=21.5f;
            LineProjectileEmissionModel voidRayBeamEmiss=voidRayBeamWeapon.emission.Cast<LineProjectileEmissionModel>();
            voidRayBeamEmiss.useTowerRotation=true;
            voidRayBeamEmiss.dontUseTowerPosition=true;
            voidRayBeamEmiss.displayPath=new(Name+"-Beam",new(Name+"-Beam"));
            voidRayBeamWeapon.projectile.behaviors.GetModel<DamageModel>().damage=1;
            voidRay.behaviors=voidRayBehav.ToArray();
            SetSounds(voidRay,Name+"-",true,true,true,false);
            return voidRay;
        }
        public TowerModel PrismaticRange(){
            TowerModel voidRay=Base().Clone<TowerModel>();
            voidRay.name=Name+"-100";
            voidRay.tier+=1;
			voidRay.tiers[0]+=1;
            voidRay.appliedUpgrades=new[]{UpgradeModels[0].name};
            voidRay.range+=10;
            voidRay.behaviors.GetModel<AttackModel>().range=voidRay.range;
            voidRay.upgrades=new UpgradePathModel[]{new(UpgradeModels[1].name,Name+"-200")};
            return voidRay;
        }
        public TowerModel FluxVanes(){
            TowerModel voidRay=PrismaticRange().Clone<TowerModel>();
            voidRay.name=Name+"-200";
            voidRay.tier+=1;
			voidRay.tiers[0]+=1;
            voidRay.appliedUpgrades=new[]{UpgradeModels[0].name,UpgradeModels[1].name};
            voidRay.behaviors.GetModel<AttackModel>().weapons[0].rate-=0.01f;
            voidRay.upgrades=new UpgradePathModel[]{new(UpgradeModels[2].name,Name+"-300")};
            return voidRay;
        }
        public TowerModel PrismaticAlignment(){
            TowerModel voidRay=FluxVanes().Clone<TowerModel>();
            voidRay.name=Name+"-300";
            voidRay.tier+=1;
			voidRay.tiers[0]+=1;
            voidRay.appliedUpgrades=new[]{UpgradeModels[0].name,UpgradeModels[1].name,UpgradeModels[2].name};
            voidRay.upgrades=new UpgradePathModel[]{new(UpgradeModels[3].name,Name+"-400")};
            Il2CppReferenceArray<Model>voidRayBehav=voidRay.behaviors;
            ProjectileModel beamProj=voidRay.behaviors.GetModel<AttackModel>().weapons[0].projectile;
            List<Model>beamProjBehav=voidRay.behaviors.GetModel<AttackModel>().weapons[0].projectile.behaviors.ToList();
            beamProjBehav.Add(new DamageModifierForTagModel("DmgForTagModel","Moabs",3,0,false,true));
            beamProj.behaviors=beamProjBehav.ToArray();
            SetUpgradeSounds(voidRayBehav.GetModel<CreateSoundOnUpgradeModel>(),Name+"-Destroyer");
            return voidRay;
        }
        public TowerModel Destroyer(){
            TowerModel voidRay=PrismaticAlignment().Clone<TowerModel>();
            voidRay.name=Name+"-400";
            voidRay.tier+=1;
			voidRay.tiers[0]+=1;
            voidRay.display=new(Name+"-DestroyerPrefab");
            voidRay.portrait=new("Ui["+Name+"-DestroyerPortrait]");
            voidRay.appliedUpgrades=new[]{UpgradeModels[0].name,UpgradeModels[1].name,UpgradeModels[2].name,UpgradeModels[3].name};
            voidRay.upgrades=new UpgradePathModel[]{new(UpgradeModels[4].name,Name+"-500")};
            Il2CppReferenceArray<Model>voidRayBehav=voidRay.behaviors;
            WeaponModel beamWeapon=voidRayBehav.GetModel<AttackModel>().weapons[0];
            beamWeapon.emission.Cast<LineProjectileEmissionModel>().displayPath=new(Name+"-DestroyerBeam",new(Name+"VoidRay-DestroyerBeam"));
            ProjectileModel beamProj=beamWeapon.projectile;
            List<Model>beamProjBehav=beamProj.behaviors.ToList();
            beamProjBehav.Add(gameModel.GetTowerFromId("BombShooter").behaviors.GetModel<AttackModel>().weapons[0].projectile.behaviors.
                GetModelClone<CreateProjectileOnContactModel>());
            beamProjBehav.GetModel<CreateProjectileOnContactModel>().projectile=gameModel.GetTowerFromId("Druid-200").behaviors.
                GetModel<AttackModel>().weapons[1].projectile.Clone<ProjectileModel>();
            Il2CppReferenceArray<Model>beamBehav=beamProjBehav.GetModel<CreateProjectileOnContactModel>().projectile.behaviors;
            LightningModel lightning=beamBehav.GetModel<LightningModel>();
            lightning.splits=1;
            lightning.splitRange=10;
            beamBehav.GetModel<DamageModel>().damage+=1f;
            CreateLightningEffectModel beamEffect=beamBehav.GetModel<CreateLightningEffectModel>();
            beamEffect.lifeSpan=0.1f;
            Il2CppReferenceArray<PrefabReference>beamEffectDisplays=beamEffect.displayPaths;
            for(int i=0;i<beamEffectDisplays.Count();i++){
                if(i>5){
                    beamEffectDisplays[i]=new(Name+"-BeamBounceLarge");
                    continue;
                }
                if(i>2){
                    beamEffectDisplays[i]=new(Name+"-BeamBounceMedium");
                    continue;
                }
                if(i>=0){
                    beamEffectDisplays[i]=new(Name+"-BeamBounceSmall");
                    continue;
                }
            }
            beamProj.behaviors=beamProjBehav.ToArray();
            SetSounds(voidRay,Name+"-Destroyer",true,true,false,false);
            SetUpgradeSounds(voidRayBehav.GetModel<CreateSoundOnUpgradeModel>(),Name+"-Mohandar");
            return voidRay;
        }
        public TowerModel Mohandar(){
            TowerModel voidRay=Destroyer().Clone<TowerModel>();
            voidRay.name=Name+"-500";
            voidRay.tier+=1;
			voidRay.tiers[0]+=1;
            voidRay.display=new(Name+"-MohandarPrefab");
            voidRay.portrait=new("Ui["+Name+"-MohandarPortrait]");
            voidRay.appliedUpgrades=new[]{UpgradeModels[0].name,UpgradeModels[1].name,UpgradeModels[2].name,UpgradeModels[3].name,
                UpgradeModels[4].name};
            voidRay.upgrades=new(0);
            Il2CppReferenceArray<Model>voidRayBehav=voidRay.behaviors;
            WeaponModel beamWeapon=voidRayBehav.GetModel<AttackModel>().weapons[0];
            beamWeapon.emission.Cast<LineProjectileEmissionModel>().displayPath=new(Name+"-MohandarBeam",new(Name+"-MohandarBeam"));
            ProjectileModel beamProj=beamWeapon.projectile;
            List<Model>beamProjBehav=beamProj.behaviors.ToList();
            beamProjBehav.RemoveModel<CreateProjectileOnContactModel>();
            beamProjBehav.GetModel<DamageModel>().damage=5;
            beamProjBehav.GetModel<DamageModifierForTagModel>().damageMultiplier+=3;
            beamProj.behaviors=beamProjBehav.ToArray();
            voidRayBehav.GetModel<DisplayModel>().display=voidRay.display;
            SetSounds(voidRay,Name+"-Mohandar",true,true,false,false);
            return voidRay;
        }
        public override void Attack(Weapon weapon){
            try{
                VoidRayCom com=weapon.attack.tower.Node.graphic.GetComponent<VoidRayCom>();
                if(com.Weight>-3){
                    com.Anim.SetLayerWeight(1,com.Weight-=0.1f);
                }
            }catch{}
        }
    }
}