# Lista de estadísticas

ID de estadísticas que se usan en el `stats` de un traje y en el código de los mods. Los valores son enteros con el formato `"ID/valor"`, y la columna Unidad muestra cómo se convierten al mostrarse en el juego. Por ejemplo, escribir `CRITICAL/-400` en una estadística donde `100 = 1%` se muestra como -4%.

Ejemplos que usan realmente los trajes integrados: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Nombre en inglés | Nombre | Unidad |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Velocidad de Ataque | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Daño de Ataque Normal | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Consigue {VALUE} dados al derrotar a un jefe | Entero |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Duración de los Beneficios aplicados por sí mismo | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Daño de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | duración &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Velocidad de ataque de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | Entero |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Velocidad de carga &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Daño de &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Bonificación &lt;tag=FinalDamage&gt; por cada tipo de combo activado | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Entero |
| `CRITICAL` | Critical Chance | Probabilidad Crítica | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Daño Crítico | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Capacidad del cargador | Entero |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Velocidad de disparo de ballesta | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Cantidad de cargadores | Entero |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Velocidad de recarga de la ballesta | Entero |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Daño extra de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Velocidad de recuperación durante la batalla &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Velocidad de consumo de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Daño de Ataque de Embestida | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Ignorar Defensa de Ataque de Embestida | 1 = 1% |
| `DASH_COUNT` | Dash Count | Cantidad de Embestidas | Entero |
| `DASH_DISABLE` | Dash Disabled | Embestida Desactivada | Entero |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bonificación de ventana de invencibilidad durante la embestida | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Velocidad de Embestida | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Daño infligido con &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Duración de todos los Perjuicios infligidos a los enemigos | 1 = 1% |
| `DEFENSE` | Defense | Defensa | Entero |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | No se aplica la reducción de daño de &lt;tag=Defense&gt;, pero el daño infligido aumenta según el incremento de &lt;tag=DamageReduction&gt; | Entero |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Capacidad de &lt;tag=TEXT:DimensionPocket&gt; | Entero |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} de probabilidad de infligir el Perjuicio &lt;tag=Burn&gt; al realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} de probabilidad de infligir el Perjuicio &lt;tag=Electric&gt; al realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} de probabilidad de infligir el Perjuicio &lt;tag=Frostbite&gt; al realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; ya no aparece | Entero |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Daño adicional de &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | Entero |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Daño adicional infligido a &lt;tag=EnemyType_Boss&gt; y &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Evasión | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; no disponible | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Reducción de probabilidad de evasión | 1 = 1% |
| `EXP_DROP` | Experience Drop | Ganancia de Experiencia | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Opciones adicionales de objetos | Entero |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Revive con 60% de PV al morir ({VALUE} veces) | Entero |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Opciones adicionales de milagro | Entero |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Opciones de armas adicionales | Entero |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | PV Total | 1 = 1% |
| `FINAL_MP` | Final MP | PM Total | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Daño de &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Daño de Fuego | Entero |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt; Amplificación | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Cantidad de embestidas fijada en {VALUE} | Entero |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Activa &lt;tag=FlameSword&gt; {VALUE} veces más | Entero |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Al infligir daño de &lt;tag=Magic:noicon&gt;, activa &lt;tag=FlameSword&gt; {VALUE} veces más | Entero |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Al infligir daño de &lt;tag=Weapon:noicon&gt;, activa &lt;tag=FlameSword&gt; {VALUE} veces más | Entero |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Tiempo de enfriamiento de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Probabilidad de golpe crítico de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Daño de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; ignora {VALUE} de la defensa del objetivo | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | Al lanzar &lt;tag=FlameSword&gt;, tienes una probabilidad de {VALUE} de aumentar su tamaño y aumentar el daño en un &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Recuento máximo de &lt;tag=FlameSword&gt; | Entero |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Velocidad de ataque del compañero | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Probabilidad de Crítico del Compañero | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Daño del Compañero | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Defensa del Compañero | Entero |
| `FOLLOWER_MAX_HP` | Companion Max HP | PV Máximo del Compañero | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Reducción del Tiempo de Resurrección del Compañero | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Daño del perjuicio &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Los objetivos afectados por &lt;tag=Frostbite&gt; reciben cada segundo un daño equivalente al {VALUE} % de &lt;tag=IceDamage&gt; (el daño aumenta con &lt;tag=Stacks&gt;) | Entero |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Daño crítico de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Daño de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Límite de brochetas de frutas | Entero |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Duración de invencibilidad durante &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Maldición de curación: la curación recibida se reduce en {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Mayor daño elemental | Entero |
| `HP_CURSE` | Max HP set to {VALUE} | PV Máx establecido en {VALUE} | Entero |
| `HP_POTION_BONUS` | HP Potion Bonus | Bono de Pociones de PV | 100 = 1% |
| `HP_STEAL` | Life Steal | Robar Vida | Entero |
| `ICE_DAMAGE` | Cold Damage | Daño por Frío | Entero |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt; Amplificación | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Ignorar Defensa | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Daño adicional al &lt;tag=SuperArmor&gt; del objetivo | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Impulso Infinito | Entero |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Reflujo de magia: &lt;tag=MaxMP&gt; se fija en &lt;tag=CONST:infinityMPMax&gt; y no disminuye | Entero |
| `INVENTORY_SLOT` | Inventory Slot | Ranura de Inventario | Entero |
| `LEAF_DROP` | Leaf Drop | Caída de Hojas | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | PV Recuperado al Subir de Nivel | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Daño por Rayo | Entero |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt; Amplificación | 1 = 1% |
| `LUCK` | Luck | Suerte | Entero |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Reducir el coste de MP de &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Probabilidad de crítico de &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Daño Crítico de &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Daño de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Ignorar Defensa de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | PV Máximo | Entero |
| `MAX_MP` | Max MP | PM Máximo | Entero |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Capacidad de &lt;tag=DarkCloud&gt; | Entero |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Capacidad de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Velocidad de Movimiento | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Regeneración de PM | Entero |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Recupera &lt;tag=MP&gt; {VALUE} al matar | Entero |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Eficiencia de recuperación de &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Daño de habilidades que consumen PM | 1 = 1% |
| `MP_STEAL` | MP Steal | Robo de PM | Entero |
| `NEGOTIATION` | Barter | Negociación | Entero |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Desactivar Invencibilidad al Esprintar | Entero |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Los libros de hechizos no consumen PM | Entero |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Otorga Beneficios a los jugadores dentro de 5 &lt;tag=Tiles&gt; al lanzar Beneficios. Sin embargo, el coste del hechizo aumenta un 50%. | Entero |
| `PHYSICAL_DAMAGE` | Physical Damage | Daño Físico | Entero |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt; Amplificación | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Velocidad de ataque planetario | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Daño Planetario | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Ranura de bolsa de pociones | Entero |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Reducción de daño recibido | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Reduce el tiempo para revivir a otro jugador en {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Bono por adquisición de zafiro | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Daño de Ataque Especial | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | Al inicio de la expedición, &lt;tag=Leaf:nocolor&gt;: {VALUE} | Entero |
| `SUB_BAG_SLOT` | Side Bag Slot | Ranura de bolsa secundaria | Entero |
| `THORNS` | Thorns | Espinas | 1 = 1% |
| `TOUGHNESS` | Toughness | Robustez | Entero |
| `TRUE_DAMAGE` | True Damage | Daño Verdadero | Entero |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Por cada combo excedente, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Probabilidad de golpe crítico de &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Rango de ataque cuerpo a cuerpo | 1 = 1% |
