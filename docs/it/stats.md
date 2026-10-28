# Elenco delle statistiche

ID delle statistiche usati in `stats` di un costume e nel codice dei mod. I valori sono numeri interi nel formato `"ID/valore"`, e la colonna Unità mostra come vengono convertiti quando compaiono nel gioco. Per esempio, scrivendo `CRITICAL/-400` per una statistica con `100 = 1%` si ottiene -4% a schermo.

Esempi usati davvero dai costumi integrati: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Nome inglese | Nome | Unità |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Velocità d'attacco | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Danni degli attacchi normali | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Ottieni {VALUE} dadi sconfiggendo un boss | Intero |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Durata dei potenziamenti applicati da sé | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Danni da &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | Durata di &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Velocità d'attacco di &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Stacks&gt; di &lt;tag=Burn&gt; | Intero |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Velocità di carica di &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Danni da &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Bonus a &lt;tag=FinalDamage&gt; per ogni tipo di combo attivato | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Intero |
| `CRITICAL` | Critical Chance | Probabilità di critico | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Danni critici | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Capacità del caricatore | Intero |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Cadenza di tiro della balestra | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Numero di caricatori | Intero |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Velocità di ricarica della balestra | Intero |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Danni extra di &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Velocità di recupero di &lt;tag=DarkCloud&gt; in combattimento | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Velocità di consumo di &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Danni degli attacchi in scatto | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Ignora difesa degli attacchi in scatto | 1 = 1% |
| `DASH_COUNT` | Dash Count | Numero di scatti | Intero |
| `DASH_DISABLE` | Dash Disabled | Scatto disabilitato | Intero |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bonus alla finestra d'invincibilità durante lo scatto | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Velocità di scatto | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Danni inflitti con &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Durata di tutti i malus inflitti ai nemici | 1 = 1% |
| `DEFENSE` | Defense | Difesa | Intero |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt; non riduce i danni, ma aumenta i danni inflitti in base all'incremento di &lt;tag=DamageReduction&gt; | Intero |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Capienza di &lt;tag=TEXT:DimensionPocket&gt; | Intero |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} di probabilità di infliggere il malus &lt;tag=Burn&gt; con &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | {VALUE} di probabilità di infliggere il malus &lt;tag=Electric&gt; con &lt;tag=WeaponAction_DirectAttack&gt;; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | {VALUE} di probabilità di infliggere il malus &lt;tag=Frostbite&gt; con &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; non appare più | Intero |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Danni extra di &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Stacks&gt; di &lt;tag=Electric&gt; | Intero |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Danni bonus inflitti a &lt;tag=EnemyType_Boss&gt; e &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Evasione | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; disabilitata | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Riduzione probabilità di schivata | 1 = 1% |
| `EXP_DROP` | Experience Drop | Esperienza ottenuta | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Scelte oggetto aggiuntive | Intero |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Rianimazione con il 60% dei PS alla morte ({VALUE} volte) | Intero |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Scelte miracolo aggiuntive | Intero |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Scelte arma aggiuntive | Intero |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | PS finali | 1 = 1% |
| `FINAL_MP` | Final MP | PM finali | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Danni di &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Danni da fuoco | Intero |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | Amplificazione &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Numero di scatti fissato a {VALUE} | Intero |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Attiva inoltre &lt;tag=FlameSword&gt; {VALUE} volte | Intero |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Infliggendo danni &lt;tag=Magic:noicon&gt;, attiva inoltre &lt;tag=FlameSword&gt; {VALUE} volte | Intero |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Infliggendo danni &lt;tag=Weapon:noicon&gt;, attiva inoltre &lt;tag=FlameSword&gt; {VALUE} volte | Intero |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Ricarica di &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Probabilità di critico di &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Danni di &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; ignora {VALUE} della difesa del bersaglio | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | Quando viene scagliata, &lt;tag=FlameSword&gt; ha una probabilità di {VALUE} di ingrandirsi e aumentare i danni del &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Numero massimo di &lt;tag=FlameSword&gt; | Intero |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Velocità d'attacco del compagno | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Probabilità di critico del compagno | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Danni del compagno | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Difesa del compagno | Intero |
| `FOLLOWER_MAX_HP` | Companion Max HP | PV massimi del compagno | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Velocità di rianimazione del compagno | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Danni da malus &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | I bersagli affetti da &lt;tag=Frostbite&gt; subiscono ogni secondo danni pari al {VALUE}% di &lt;tag=IceDamage&gt; (i danni aumentano con gli &lt;tag=Stacks&gt;) | Intero |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Danni critici della &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Danni della &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Spiedini di frutta max | Intero |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Durata dell'invincibilità durante &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Maledizione curativa: le cure ricevute diminuiscono di {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Danno elementale più alto | Intero |
| `HP_CURSE` | Max HP set to {VALUE} | PS max fissati a {VALUE} | Intero |
| `HP_POTION_BONUS` | HP Potion Bonus | Bonus pozione PS | 100 = 1% |
| `HP_STEAL` | Life Steal | Assorbimento vitale | Intero |
| `ICE_DAMAGE` | Cold Damage | Danni da freddo | Intero |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Amplificazione dei &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Ignora difesa | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Danni extra alla &lt;tag=SuperArmor&gt; del bersaglio | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Scatto infinito | Intero |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Riflusso magico: i &lt;tag=MaxMP&gt; sono fissati a &lt;tag=CONST:infinityMPMax&gt; e gli MP non diminuiscono | Intero |
| `INVENTORY_SLOT` | Inventory Slot | Slot inventario | Intero |
| `LEAF_DROP` | Leaf Drop | Rilascio foglie | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | PS ripristinati salendo di livello | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Danni da fulmine | Intero |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | Amplificazione dei &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | Fortuna | Intero |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Riduzione costo in MP di &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Probabilità di critico &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Danni critici &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Danni &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Ignora difesa &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | PV massimi | Intero |
| `MAX_MP` | Max MP | MP massimi | Intero |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Capacità di &lt;tag=DarkCloud&gt; | Intero |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Capacità di &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Velocità di movimento | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Rigenerazione MP | Intero |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Recupera {VALUE} &lt;tag=MP&gt; all'uccisione | Intero |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Efficacia del recupero di &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Danni delle abilità che consumano MP | 1 = 1% |
| `MP_STEAL` | MP Steal | Furto MP | Intero |
| `NEGOTIATION` | Barter | Contrattazione | Intero |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Disattiva invincibilità durante lo scatto | Intero |
| `NO_MAGIC_COST` | Grimoires do not consume MP | I grimori non consumano MP | Intero |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Conferisce potenziamenti ai giocatori entro 5 &lt;tag=Tiles&gt; quando lanci potenziamenti. Tuttavia, il costo dell'incantesimo aumenta del 50%. | Intero |
| `PHYSICAL_DAMAGE` | Physical Damage | Danni fisici | Intero |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | Amplificazione &lt;tag=PhysicalDamage&gt; | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Velocità d'attacco dei pianeti | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Danni dei pianeti | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Slot borsa delle pozioni | Intero |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Riduzione danni subiti | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Riduce di {VALUE} il tempo necessario per rianimare un altro giocatore | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Bonus acquisizione zaffiri | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Danni attacco speciale | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | Inizia con {VALUE} &lt;tag=Leaf:nocolor&gt; | Intero |
| `SUB_BAG_SLOT` | Side Bag Slot | Slot borsa laterale | Intero |
| `THORNS` | Thorns | Spine | 1 = 1% |
| `TOUGHNESS` | Toughness | Tenacia | Intero |
| `TRUE_DAMAGE` | True Damage | Danni puri | Intero |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Per ogni combo oltre il limite, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Probabilità di colpo critico di &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Portata attacco in mischia | 1 = 1% |
