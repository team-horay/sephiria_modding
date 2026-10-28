# Lista de atributos

IDs de atributos usados no `stats` de um traje e no código de mods. Os valores são inteiros no formato `"ID/valor"`, e a coluna Unidade mostra como eles são convertidos ao serem exibidos no jogo. Por exemplo, escrever `CRITICAL/-400` em um atributo cuja unidade é `100 = 1%` é exibido como -4%.

Exemplos usados de fato pelos trajes padrão do jogo: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | Nome em inglês | Nome | Unidade |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Velocidade de ataque | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Dano do ataque normal | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Adquire {VALUE} dado(s) ao derrotar um chefe | Inteiro |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Duração dos Buffs aplicados pelo usuário | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | Dano adicional de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | Duração de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | Velocidade de ataque de &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Stacks&gt; de &lt;tag=Burn&gt; | Inteiro |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | Velocidade de carga de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | Dano de &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Bônus de &lt;tag=FinalDamage&gt; para cada tipo de combo ativado | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Inteiro |
| `CRITICAL` | Critical Chance | Chance crítica | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Dano crítico | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Capacidade do cartucho | Inteiro |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Velocidade de disparo da besta | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Número de cartuchos | Inteiro |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Velocidade de recarga da besta | Inteiro |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Dano adicional para &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Taxa de recuperação de &lt;tag=DarkCloud&gt; durante a batalha | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | Velocidade de consumo de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Dano do ataque de arrancada | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Ignorar defesa do ataque de arrancada | 1 = 1% |
| `DASH_COUNT` | Dash Count | Número de arrancadas | Inteiro |
| `DASH_DISABLE` | Dash Disabled | Arrancada desativada | Inteiro |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Bônus da janela de invencibilidade ao usar a arrancada | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Velocidade da arrancada | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | Dano causado com &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Duração de todos os debuffs infligidos em inimigos | 1 = 1% |
| `DEFENSE` | Defense | Defesa | Inteiro |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt; não reduz o dano recebido, mas o dano causado aumenta pela quantia de &lt;tag=DamageReduction&gt; aumentada | Inteiro |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | Capacidade de &lt;tag=TEXT:DimensionPocket&gt; | Inteiro |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | Chance de {VALUE} de infligir o debuff de &lt;tag=Burn&gt; ao realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | Chance de {VALUE} de infligir o debuff de &lt;tag=Electric&gt; ao realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | Chance de {VALUE} de infligir o debuff de &lt;tag=Frostbite&gt; ao realizar &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; não aparece mais | Inteiro |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Dano adicional de &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Stacks&gt; de &lt;tag=Electric&gt; | Inteiro |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | Dano adicional causado a &lt;tag=EnemyType_Boss&gt; e &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | Desvio | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; indisponível | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Redução da chance de evasão | 1 = 1% |
| `EXP_DROP` | Experience Drop | Queda de experiência | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Escolhas de itens adicionais | Inteiro |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Reviva com 60% dos PV ao morrer ({VALUE} vezes) | Inteiro |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Escolhas de milagres adicionais | Inteiro |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Escolhas de armas adicionais | Inteiro |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | PV total | 1 = 1% |
| `FINAL_MP` | Final MP | PM total | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | Dano de &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Dano de fogo | Inteiro |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | Amplificação de &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Contador de arrancadas fixado em {VALUE} | Inteiro |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Ativa &lt;tag=FlameSword&gt; {VALUE} vezes adicionais | Inteiro |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Ao causar dano de &lt;tag=Magic:noicon&gt;, ativa &lt;tag=FlameSword&gt; {VALUE} vezes adicionais | Inteiro |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Ao causar dano de &lt;tag=Weapon:noicon&gt;, ativa &lt;tag=FlameSword&gt; {VALUE} vezes adicionais | Inteiro |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | Recarga de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | Chance crítica de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | Dano de &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | Ignora a defesa do alvo em {VALUE} ao usar &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | A &lt;tag=FlameSword&gt; tem {VALUE} de chance de aumentar o tamanho e o dano em &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% ao ser arremessada | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Contagem máxima de &lt;tag=FlameSword&gt; | Inteiro |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Velocidade de ataque do companheiro | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Chance crítica do companheiro | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Dano do companheiro | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Defesa do companheiro | Inteiro |
| `FOLLOWER_MAX_HP` | Companion Max HP | PV máx. do companheiro | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Velocidade do tempo de renascimento do companheiro | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | Dano de efeito negativo de &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | Os alvos infligidos por &lt;tag=Frostbite&gt; recebem por segundo dano equivalente a {VALUE}% de &lt;tag=IceDamage&gt; (o dano aumenta com os &lt;tag=Stacks&gt;) | Inteiro |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | Dano crítico de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | Dano de &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Limite de espetinhos de frutas | Inteiro |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | Duração de invencibilidade durante &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | Maldição de cura: a cura recebida diminui em {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | Maior dano elemental | Inteiro |
| `HP_CURSE` | Max HP set to {VALUE} | PV máx. definidos em {VALUE} | Inteiro |
| `HP_POTION_BONUS` | HP Potion Bonus | Bônus da poção de PV | 100 = 1% |
| `HP_STEAL` | Life Steal | Roubo de vida | Inteiro |
| `ICE_DAMAGE` | Cold Damage | Dano gélido | Inteiro |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | Amplificação de &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Ignorar defesa | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Dano adicional ao alvo de &lt;tag=SuperArmor&gt; | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Arrancada infinita | Inteiro |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Refluxo de mana: &lt;tag=MaxMP&gt; é fixado em &lt;tag=CONST:infinityMPMax&gt; e não diminui | Inteiro |
| `INVENTORY_SLOT` | Inventory Slot | Espaço de inventário | Inteiro |
| `LEAF_DROP` | Leaf Drop | Chance de encontrar folhas | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | PV restaurados ao subir de nível | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Dano elétrico | Inteiro |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | Amplificação de &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | Sorte | Inteiro |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | Reduzir o custo de MP de &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | Chance crítica de &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | Dano crítico do &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | Dano de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | Ignorar defesa de &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | PV máx. | Inteiro |
| `MAX_MP` | Max MP | PM máx. | Inteiro |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | Capacidade de &lt;tag=DarkCloud&gt; | Inteiro |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | Capacidade de &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Velocidade de movimento | 1 = 1% |
| `MP_REGEN` | MP Regeneration | Regeneração dos PM | Inteiro |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Restaura {VALUE} de &lt;tag=MP&gt; em cada abate | Inteiro |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | Eficiência de recuperação de &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | Dano das habilidades que consomem PM | 1 = 1% |
| `MP_STEAL` | MP Steal | Roubo dos PM | Inteiro |
| `NEGOTIATION` | Barter | Negociação | Inteiro |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Desativação da invencibilidade da arrancada | Inteiro |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Os grimórios não consomem PM | Inteiro |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Concede buffs aos jogadores dentro de 5 &lt;tag=Tiles&gt; ao lançar os buffs. Entretanto, o custo do feitiço é aumentado em 50%. | Inteiro |
| `PHYSICAL_DAMAGE` | Physical Damage | Dano físico | Inteiro |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | Amplificação de &lt;tag=PhysicalDamage&gt; | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Velocidade de ataque do planeta | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Dano do planeta | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | Slot de bolsa de poção | Inteiro |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Redução de dano recebido | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Reduz o tempo necessário para reviver outro jogador em {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Bônus de aquisição de safira | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Dano do ataque especial | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | No início da expedição, &lt;tag=Leaf:nocolor&gt;: {VALUE} | Inteiro |
| `SUB_BAG_SLOT` | Side Bag Slot | Espaço de bolsa secundária | Inteiro |
| `THORNS` | Thorns | Espinhos | 1 = 1% |
| `TOUGHNESS` | Toughness | Resistência | Inteiro |
| `TRUE_DAMAGE` | True Damage | Dano verdadeiro | Inteiro |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Para cada combo excedente, &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | Chance de acerto crítico de &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Alcance de ataque corpo a corpo | 1 = 1% |
