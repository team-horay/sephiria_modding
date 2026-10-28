# Liste des stats

ID des stats utilisés dans le champ `stats` d'une tenue et dans le code des mods. Les valeurs sont des entiers au format `"ID/valeur"`, et la colonne Unité indique comment elles sont converties à l'affichage en jeu. Par exemple, écrire `CRITICAL/-400` pour une stat où `100 = 1%` s'affiche comme -4 %.

Exemples réellement utilisés par les tenues intégrées : `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Nom anglais | Nom | Unité |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Vitesse d’attaque | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Dégâts d’attaque normale | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Obtenez {VALUE} dés en vainquant un boss | Entier |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Durée des bonus appliqués par vous-même | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Dégâts de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | Durée de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Vitesse d’attaque de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | Entier |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Vitesse de charge de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Dégâts des &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Bonus de &lt;tag=FinalDamage&gt; pour chaque type de combo activé | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Entier |
| `CRITICAL` | Critical Chance | Chances de coup critique | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Dégâts critiques | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Capacité du chargeur | Entier |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Vitesse de tir de l’arbalète | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Nombre de chargeurs | Entier |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Vitesse de rechargement de l’arbalète | Entier |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Dégâts supplémentaires de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Taux de récupération de &lt;tag=DarkCloud&gt; en combat | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Vitesse de consommation de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Dégâts d’attaque en ruée | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Pénétration de défense d’attaque en ruée | 1 = 1% |
| `DASH_COUNT` | Dash Count | Nombre de ruées | Entier |
| `DASH_DISABLE` | Dash Disabled | Ruée désactivée | Entier |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bonus de fenêtre d’invincibilité pendant la ruée | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Vitesse de ruée | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Dégâts infligés avec &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Durée de tous les Malus infligés aux ennemis | 1 = 1% |
| `DEFENSE` | Defense | Défense | Entier |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | La &lt;tag=Defense&gt; ne réduit pas les dégâts, mais augmente les dégâts infligés en fonction de l’augmentation de &lt;tag=DamageReduction&gt; | Entier |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Capacité de &lt;tag=TEXT:DimensionPocket&gt; | Entier |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} de chances d’infliger le Malus &lt;tag=Burn&gt; avec &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} de chances d’infliger le Malus &lt;tag=Electric&gt; avec &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} de chances d’infliger le Malus &lt;tag=Frostbite&gt; avec &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; n’apparaît plus | Entier |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Dégâts supplémentaires de &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | Entier |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Dégâts supplémentaires infligés aux &lt;tag=EnemyType_Boss&gt; et &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Esquive | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; désactivée | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Réduction des chances d’esquive | 1 = 1% |
| `EXP_DROP` | Experience Drop | Gain d’expérience | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Choix d’objets supplémentaires | Entier |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Ressuscite avec 60 % de PV à la mort ({VALUE} fois) | Entier |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Choix de miracles supplémentaires | Entier |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Choix d’armes supplémentaires | Entier |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | PV finaux | 1 = 1% |
| `FINAL_MP` | Final MP | PM finaux | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Dégâts de &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Dégâts de feu | Entier |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | Amplification de &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Nombre de ruées fixé à {VALUE} | Entier |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Active &lt;tag=FlameSword&gt; {VALUE} fois supplémentaires | Entier |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Lorsque des dégâts de &lt;tag=Magic:noicon&gt; sont infligés, active &lt;tag=FlameSword&gt; {VALUE} fois supplémentaires | Entier |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Lorsque des dégâts de &lt;tag=Weapon:noicon&gt; sont infligés, active &lt;tag=FlameSword&gt; {VALUE} fois supplémentaires | Entier |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Temps de recharge de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Chances de coup critique de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Dégâts de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | Ignore {VALUE} de la défense de la cible avec &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | Lorsqu’elle est lancée, la &lt;tag=FlameSword&gt; a {VALUE} de chances de grandir et d’infliger &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt; % de dégâts supplémentaires | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Nombre max. de &lt;tag=FlameSword&gt; | Entier |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Vitesse d’attaque du compagnon | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Chance de coup critique du compagnon | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Dégâts du compagnon | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Défense des compagnons | Entier |
| `FOLLOWER_MAX_HP` | Companion Max HP | PV max des compagnons | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Accélération de réanimation du compagnon | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Dégâts de malus de &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Les cibles affectées par &lt;tag=Frostbite&gt; subissent chaque seconde des dégâts égaux à {VALUE} % de &lt;tag=IceDamage&gt; (les dégâts augmentent avec les &lt;tag=Stacks&gt;) | Entier |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Dégâts critiques de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Dégâts de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Maximum de brochettes de fruits | Entier |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Durée d’invincibilité pendant &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Malédiction de soin : soins reçus réduits de {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Dégâts élémentaires les plus élevés | Entier |
| `HP_CURSE` | Max HP set to {VALUE} | PV max. fixés à {VALUE} | Entier |
| `HP_POTION_BONUS` | HP Potion Bonus | Bonus de potion de PV | 100 = 1% |
| `HP_STEAL` | Life Steal | Vol de vie | Entier |
| `ICE_DAMAGE` | Cold Damage | Dégâts de froid | Entier |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Amplification des &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Pénétration de défense | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Dégâts supplémentaires au &lt;tag=SuperArmor&gt; de la cible | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Ruée infinie | Entier |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Flux magique : &lt;tag=MaxMP&gt; est fixé à &lt;tag=CONST:infinityMPMax&gt;, et les PM ne diminuent pas | Entier |
| `INVENTORY_SLOT` | Inventory Slot | Emplacement d’inventaire | Entier |
| `LEAF_DROP` | Leaf Drop | Chute de feuilles | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | PV récupérés à chaque niveau | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Dégâts de foudre | Entier |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | Amplification des &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | Chance | Entier |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Réduit le coût en PM de &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Chance de critique de &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Dégâts critiques de &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Dégâts de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Pénétration de défense de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | PV max | Entier |
| `MAX_MP` | Max MP | PM max | Entier |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Capacité de &lt;tag=DarkCloud&gt; | Entier |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Capacité de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Vitesse de déplacement | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Régénération de PM | Entier |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Récupère {VALUE} &lt;tag=MP&gt; à chaque élimination | Entier |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Efficacité lors de la récupération de &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Dégâts des capacités consommant des PM | 1 = 1% |
| `MP_STEAL` | MP Steal | Vol de PM | Entier |
| `NEGOTIATION` | Barter | Troc | Entier |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Désactive l’invincibilité pendant la ruée | Entier |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Les grimoires ne consomment pas de PM | Entier |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Lorsque vous lancez un sort de bonus, les joueurs dans un rayon de 5 &lt;tag=Tiles&gt; reçoivent également ce bonus. En contrepartie, le coût d’incantation augmente de 50 %. | Entier |
| `PHYSICAL_DAMAGE` | Physical Damage | Dégâts physiques | Entier |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | Amplification de &lt;tag=PhysicalDamage&gt; | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Vitesse d’attaque des planètes | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Dégâts des planètes | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Emplacement de la sacoche à potions | Entier |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Réduction des dégâts subis | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Réduit de {VALUE} le temps nécessaire pour ranimer un autre joueur | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Bonus d’acquisition de saphirs | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Dégâts d’attaque spéciale | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | Au début de l’expédition, &lt;tag=Leaf:nocolor&gt; : {VALUE} | Entier |
| `SUB_BAG_SLOT` | Side Bag Slot | Emplacement de sacoche latérale | Entier |
| `THORNS` | Thorns | Épines | 1 = 1% |
| `TOUGHNESS` | Toughness | Robustesse | Entier |
| `TRUE_DAMAGE` | True Damage | Dégâts bruts | Entier |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Par combo excédentaire, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Chance de coup critique de &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Portée d’attaque au corps à corps | 1 = 1% |
