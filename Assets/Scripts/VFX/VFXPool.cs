using CosmicCuration.Enemy;
using CosmicCuration.PowerUps;
using CosmicCuration.Utilities;
using System;

namespace CosmicCuration.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXView vfxView;

        public VFXController GetVFX<T>(VFXView vfxView) where T : VFXController
        {
            this.vfxView = vfxView;

            return GetItem<T>();
        }

        protected override VFXController CreateItem<T>() => new VFXController(vfxView);
    }
}