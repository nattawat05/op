using EloBuddy; 
using LeagueSharp.Common; 
namespace ReformedAIO.Champions.Annie.Drawings
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.Common;

    using ReformedAIO.Champions.Annie.Core.Damage;
    using ReformedAIO.Library.Drawings;

    using RethoughtLib.FeatureSystem.Abstract_Classes;

    using SharpDX;

    internal sealed class AnnieDamageDraw : ChildBase
    {
        public override string Name { get; set; } = "Damage";

        public readonly AnnieDamage Damage;

        public AnnieDamageDraw(AnnieDamage damage)
        {
            this.Damage = damage;
        }

        private HeroHealthBarIndicator heroHealthBarIndicator;

        public void OnDraw(EventArgs args)
        {
            if (ObjectManager.Player.IsDead) return;

            foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(ene => ene.IsValidTarget(2000)))
            {
                heroHealthBarIndicator.Unit = enemy;

                heroHealthBarIndicator.DrawDmg(Damage.GetComboDamage(enemy),
                    enemy.Health <= Damage.GetComboDamage(enemy) * .85
                    ? Color.LawnGreen
                    : Color.Yellow);
            }
        }

        protected override void OnDisable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnDisable(sender, eventArgs);

            Drawing.OnDraw -= OnDraw;
        }

        protected override void OnEnable(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnEnable(sender, eventArgs);


            Drawing.OnDraw += OnDraw;
        }

        protected override void OnLoad(object sender, FeatureBaseEventArgs eventArgs)
        {
            base.OnLoad(sender, eventArgs);

            heroHealthBarIndicator = new HeroHealthBarIndicator();
        }
    }
}
