# Werteliste

Wert-IDs, die in den `stats` eines Kostüms und im Mod-Code verwendet werden. Die Werte sind Ganzzahlen im Format `"ID/Wert"`, und die Spalte „Einheit“ zeigt, wie sie bei der Anzeige im Spiel umgerechnet werden. Schreibst du zum Beispiel `CRITICAL/-400` bei einem Wert mit `100 = 1%`, wird -4% angezeigt.

Beispiele, die eingebaute Kostüme tatsächlich verwenden: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Englisch | Name | Einheit |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Angriffsgeschwindigkeit | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Schaden normaler Angriffe | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Erhalte beim Besiegen eines Bosses {VALUE} Würfel | Ganzzahl |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Dauer selbst angewendeter Stärkungen | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt;-Schaden | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt;-Dauer | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Angriffsgeschwindigkeit von &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt;-&lt;tag=Stacks&gt; | Ganzzahl |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Aufladegeschwindigkeit von &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt;-Schaden | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | &lt;tag=FinalDamage&gt;-Bonus für jeden aktivierten Kombo-Typ | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Ganzzahl |
| `CRITICAL` | Critical Chance | Kritische Trefferchance | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Kritischer Schaden | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Magazinkapazität | Ganzzahl |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Armbrust-Feuerrate | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Magazinanzahl | Ganzzahl |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Armbrust-Nachladegeschwindigkeit | Ganzzahl |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Zusätzlicher &lt;tag=DarkCloud&gt;-Schaden | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Regenerationsrate von &lt;tag=DarkCloud&gt; im Kampf | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Verbrauchsgeschwindigkeit von &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Dash-Angriffsschaden | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Dash-Angriff: Verteidigung ignorieren | 1 = 1% |
| `DASH_COUNT` | Dash Count | Dash-Anzahl | Ganzzahl |
| `DASH_DISABLE` | Dash Disabled | Dash deaktiviert | Ganzzahl |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bonus auf das Unverwundbarkeitsfenster beim Dashen | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Dash-Geschwindigkeit | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Mit &lt;tag=Debuff&gt; verursachter Schaden | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Dauer aller Gegnern zugefügten Schwächungen | 1 = 1% |
| `DEFENSE` | Defense | Verteidigung | Ganzzahl |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | Keine Schadensverringerung durch &lt;tag=Defense&gt;, erhöht jedoch den verursachten Schaden entsprechend der erhöhten &lt;tag=DamageReduction&gt; | Ganzzahl |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Kapazität von &lt;tag=TEXT:DimensionPocket&gt; | Ganzzahl |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} Chance, bei &lt;tag=WeaponAction_DirectAttack&gt; die Schwächung &lt;tag=Burn&gt; zuzufügen | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} Chance, bei &lt;tag=WeaponAction_DirectAttack&gt; die Schwächung &lt;tag=Electric&gt; zuzufügen; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} Chance, bei &lt;tag=WeaponAction_DirectAttack&gt; die Schwächung &lt;tag=Frostbite&gt; zuzufügen | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; erscheint nicht mehr | Ganzzahl |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Zusätzlicher &lt;tag=Electric&gt;-Schaden | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt;-&lt;tag=Stacks&gt; | Ganzzahl |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Bonusschaden gegen &lt;tag=EnemyType_Boss&gt; und &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Ausweichen | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; deaktiviert | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Reduzierte Ausweichchance | 1 = 1% |
| `EXP_DROP` | Experience Drop | EP-Drop | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Zusätzliche Gegenstandsauswahlen | Ganzzahl |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Wird bei Tod mit 60 % LP wiederbelebt ({VALUE} Mal) | Ganzzahl |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Zusätzliche Wunderauswahlen | Ganzzahl |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Zusätzliche Waffenauswahlen | Ganzzahl |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | Finale LP | 1 = 1% |
| `FINAL_MP` | Final MP | Finale MP | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt;-Schaden | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Feuerschaden | Ganzzahl |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | Verstärkung von &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Dash-Anzahl auf {VALUE} festgelegt | Ganzzahl |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Aktiviert &lt;tag=FlameSword&gt; {VALUE}-mal zusätzlich | Ganzzahl |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Aktiviert &lt;tag=FlameSword&gt; {VALUE}-mal zusätzlich, wenn &lt;tag=Magic:noicon&gt;-Schaden verursacht wird | Ganzzahl |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Aktiviert &lt;tag=FlameSword&gt; {VALUE}-mal zusätzlich, wenn &lt;tag=Weapon:noicon&gt;-Schaden verursacht wird | Ganzzahl |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Abklingzeit von &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Kritische Chance von &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Schaden von &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | Ignoriert mit &lt;tag=FlameSword&gt; die Verteidigung des Ziels um {VALUE} | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt; hat eine Chance von {VALUE}, beim Werfen größer zu werden und den Schaden um &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt; % zu erhöhen | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Maximale Anzahl von &lt;tag=FlameSword&gt; | Ganzzahl |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Angriffsgeschwindigkeit des Begleiters | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Kritische Trefferchance des Begleiters | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Schaden des Begleiters | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Verteidigung des Begleiters | Ganzzahl |
| `FOLLOWER_MAX_HP` | Companion Max HP | Max. LP des Begleiters | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Wiederbelebungszeit des Begleiters beschleunigen | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Schaden der Schwächung &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Ziele mit &lt;tag=Frostbite&gt; erleiden pro Sekunde &lt;tag=IceDamage&gt; in Höhe von {VALUE} % (der Schaden steigt mit &lt;tag=Stacks&gt;) | Ganzzahl |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Kritischer Schaden von &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Schaden von &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Max. Fruchtspieße | Ganzzahl |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Unverwundbarkeitsdauer während &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Heilfluch: Erhaltene Heilung verringert sich um {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Höchster Elementarschaden | Ganzzahl |
| `HP_CURSE` | Max HP set to {VALUE} | Max. LP auf {VALUE} gesetzt | Ganzzahl |
| `HP_POTION_BONUS` | HP Potion Bonus | LP-Trankbonus | 100 = 1% |
| `HP_STEAL` | Life Steal | Lebensraub | Ganzzahl |
| `ICE_DAMAGE` | Cold Damage | Kälteschaden | Ganzzahl |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Verstärkung von &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Verteidigung ignorieren | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Zusätzlicher Schaden an der &lt;tag=SuperArmor&gt; des Ziels | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Unendlicher Dash | Ganzzahl |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Magischer Rückfluss: &lt;tag=MaxMP&gt; ist auf &lt;tag=CONST:infinityMPMax&gt; festgelegt und MP sinkt nicht | Ganzzahl |
| `INVENTORY_SLOT` | Inventory Slot | Inventarplatz | Ganzzahl |
| `LEAF_DROP` | Leaf Drop | Blätterfall | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | Bei Stufenaufstieg wiederhergestellte LP | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Blitzschaden | Ganzzahl |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt;-Verstärkung | 1 = 1% |
| `LUCK` | Luck | Glück | Ganzzahl |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | MP-Kosten von &lt;tag=Magic:noicon&gt; verringern | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt;-Kritische Trefferchance | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt;-Kritischer Schaden | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt;-Schaden | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Verteidigung ignorieren für &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | Max. LP | Ganzzahl |
| `MAX_MP` | Max MP | Max. MP | Ganzzahl |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;-Kapazität | Ganzzahl |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt;-Kapazität | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Bewegungstempo | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP-Regeneration | Ganzzahl |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Stellt beim Töten &lt;tag=MP&gt; in Höhe von {VALUE} wieder her | Ganzzahl |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Effizienz bei der Wiederherstellung von &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Schaden von Fähigkeiten, die MP verbrauchen | 1 = 1% |
| `MP_STEAL` | MP Steal | MP-Entzug | Ganzzahl |
| `NEGOTIATION` | Barter | Tauschhandel | Ganzzahl |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Unverwundbarkeit beim Dash deaktivieren | Ganzzahl |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Grimoires verbrauchen kein MP | Ganzzahl |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Verleiht Spielern innerhalb von 5 &lt;tag=Tiles&gt; beim Wirken von Stärkungszaubern ebenfalls Stärkungen. Die Zauberkosten sind jedoch um 50 % erhöht. | Ganzzahl |
| `PHYSICAL_DAMAGE` | Physical Damage | Physischer Schaden | Ganzzahl |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt;-Verstärkung | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Angriffsgeschwindigkeit von Planeten | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Planetenschaden | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Tranktaschenplatz | Ganzzahl |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Reduzierter erlittener Schaden | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Verringert die Wiederbelebungszeit anderer Spieler um {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Saphir-Erhaltsbonus | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Spezialangriffsschaden | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | Zu Beginn der Expedition {VALUE} &lt;tag=Leaf:nocolor&gt; | Ganzzahl |
| `SUB_BAG_SLOT` | Side Bag Slot | Seitentaschenplatz | Ganzzahl |
| `THORNS` | Thorns | Dornen | 1 = 1% |
| `TOUGHNESS` | Toughness | Zähigkeit | Ganzzahl |
| `TRUE_DAMAGE` | True Damage | Wahrer Schaden | Ganzzahl |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Für jede überschüssige Kombo &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Kritische Trefferchance von &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Nahkampfreichweite | 1 = 1% |
