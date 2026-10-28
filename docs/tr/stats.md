# Nitelik Listesi

Bir kostümün `stats` alanında ve mod kodunda kullanılan nitelik kimlikleri. Değerler `"ID/değer"` biçiminde tam sayılardır ve Birim sütunu, oyun içinde gösterilirken nasıl dönüştürüldüklerini gösterir. Örneğin `100 = 1%` olan bir nitelik için `CRITICAL/-400` yazmak -4% olarak gösterilir.

Yerleşik kostümlerin gerçekten kullandığı örnekler: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`.

| ID | İngilizce ad | Ad | Birim |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | Saldırı Hızı | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | Normal Saldırı Hasarı | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | Bir boss yenildiğinde {VALUE} zar kazan | Tam sayı |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | Kendine Uyguladığın Güçlendirmelerin Süresi | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | &lt;tag=Burn&gt; Hasarı | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | &lt;tag=Burn&gt; süresi | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | &lt;tag=Burn&gt; Saldırı Hızı | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | Tam sayı |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | &lt;tag=FrostRelic&gt; Dolum Hızı | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | &lt;tag=Artifact&gt; Hasarı | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | Etkinleştirilen Her Kombo Türü Başına &lt;tag=FinalDamage&gt; Bonusu | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | Tam sayı |
| `CRITICAL` | Critical Chance | Kritik Şansı | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | Kritik Hasarı | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | Şarjör Kapasitesi | Tam sayı |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | Arbalet Atış Hızı | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | Şarjör Sayısı | Tam sayı |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | Arbalet Yeniden Doldurma Hızı | Tam sayı |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | Ek &lt;tag=DarkCloud&gt; hasarı | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | Çatışma sırasında &lt;tag=DarkCloud&gt; Yenilenme Oranı | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | &lt;tag=DarkCloud&gt; Tüketim Hızı | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | Atılma Saldırısı Hasarı | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | Atılma Saldırısı Savunmayı Yok Sayma | 1 = 1% |
| `DASH_COUNT` | Dash Count | Atılma Sayısı | Tam sayı |
| `DASH_DISABLE` | Dash Disabled | Atılma Devre Dışı | Tam sayı |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | Atılma sırasında ek yenilmezlik süresi | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | Atılma hızı | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | &lt;tag=Debuff&gt; ile verilen hasar | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | Düşmanlara uygulanan tüm zayıflatmaların süresi | 1 = 1% |
| `DEFENSE` | Defense | Savunma | Tam sayı |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | &lt;tag=Defense&gt; artık hasarı azaltmaz, bunun yerine artan &lt;tag=DamageReduction&gt; kadar verilen hasarı artırır | Tam sayı |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | &lt;tag=TEXT:DimensionPocket&gt; Kapasitesi | Tam sayı |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt; ile {VALUE} ihtimalle &lt;tag=Burn&gt; zayıflatması uygular | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | &lt;tag=WeaponAction_DirectAttack&gt; ile {VALUE} ihtimalle &lt;tag=Electric&gt; zayıflatması uygular; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt; ile {VALUE} ihtimalle &lt;tag=Frostbite&gt; zayıflatması uygular | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; artık ortaya çıkmaz | Tam sayı |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | Ek &lt;tag=Electric&gt; hasarı | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | Tam sayı |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | &lt;tag=EnemyType_Boss&gt; ve &lt;tag=EnemyType_Miniboss&gt; düşmanlarına verilen ilave hasar | 1 = 1% |
| `EVASION` | Evasion | Kaçınma | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | &lt;tag=Evasion&gt; Devre Dışı | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | Kaçınma Şansı Azaltma | 1 = 1% |
| `EXP_DROP` | Experience Drop | Deneyim Kazanımı | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | Ek Eşya Seçenekleri | Tam sayı |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | Ölünce %60 Can ile diril ({VALUE} kez) | Tam sayı |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | Ek Mucize Seçenekleri | Tam sayı |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | Ek Silah Seçenekleri | Tam sayı |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | Nihai Can | 1 = 1% |
| `FINAL_MP` | Final MP | Nihai Mana | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | &lt;tag=Weapon&gt; Hasarı | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | Ateş Hasarı | Tam sayı |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | &lt;tag=FireDamage&gt; Güçlendirmesi | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | Atılma sayısı {VALUE} olarak sabitlenir | Tam sayı |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | Ek olarak &lt;tag=FlameSword&gt; {VALUE} kez etkinleşir | Tam sayı |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=Magic:noicon&gt; hasarı verirken ek olarak &lt;tag=FlameSword&gt; {VALUE} kez etkinleşir | Tam sayı |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | &lt;tag=Weapon:noicon&gt; hasarı verirken ek olarak &lt;tag=FlameSword&gt; {VALUE} kez etkinleşir | Tam sayı |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | &lt;tag=FlameSword&gt; Bekleme Süresi | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; Kritik Şansı | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | &lt;tag=FlameSword&gt; Hasarı | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | &lt;tag=FlameSword&gt; ile hedefin savunmasını {VALUE} yok say | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | &lt;tag=FlameSword&gt; fırlatıldığında {VALUE} ihtimalle büyür ve hasarı &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% artar | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | Azami &lt;tag=FlameSword&gt; Sayısı | Tam sayı |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | Yoldaş Saldırı Hızı | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | Yoldaş Kritik Şansı | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | Yoldaş Hasarı | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | Yoldaş Savunması | Tam sayı |
| `FOLLOWER_MAX_HP` | Companion Max HP | Yoldaş Maks. HP | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | Yoldaş Dirilme Hızı | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | &lt;tag=Freeze&gt; Zayıflatma Hasarı | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | &lt;tag=Frostbite&gt; etkisindeki hedefler saniye başına &lt;tag=IceDamage&gt; değerinin %{VALUE} kadarına eşit hasar alır (hasar &lt;tag=Stacks&gt; ile artar) | Tam sayı |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | &lt;tag=FrostRelic&gt; kritik hasarı | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | &lt;tag=FrostRelic&gt; hasarı | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | Maks. Meyve Şişi | Tam sayı |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | &lt;tag=WeaponAction_Fury&gt; Sırasında Dokunulmazlık Süresi | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | İyileştirme Laneti: Alınan iyileştirme {VALUE} azalır | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | En Yüksek Element Hasarı | Tam sayı |
| `HP_CURSE` | Max HP set to {VALUE} | Maks. HP {VALUE} olarak sabitlenir | Tam sayı |
| `HP_POTION_BONUS` | HP Potion Bonus | HP İksiri Bonusu | 100 = 1% |
| `HP_STEAL` | Life Steal | Can Çalma | Tam sayı |
| `ICE_DAMAGE` | Cold Damage | Soğuk Hasarı | Tam sayı |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | &lt;tag=IceDamage&gt; Artışı | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | Savunmayı Yok Sayma | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | Hedefin &lt;tag=SuperArmor&gt; değerine ek hasar | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | Sonsuz Atılma | Tam sayı |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | Büyü Akışı: &lt;tag=MaxMP&gt; &lt;tag=CONST:infinityMPMax&gt; olarak sabitlenir ve MP azalmaz | Tam sayı |
| `INVENTORY_SLOT` | Inventory Slot | Envanter Yuvası | Tam sayı |
| `LEAF_DROP` | Leaf Drop | Yaprak Düşürme | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | Seviye Atlayınca Yenilenen HP | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | Yıldırım Hasarı | Tam sayı |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | &lt;tag=LightningDamage&gt; Artışı | 1 = 1% |
| `LUCK` | Luck | Şans | Tam sayı |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | &lt;tag=Magic:noicon&gt; MP Maliyetini Azalt | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | &lt;tag=Magic&gt; Kritik Şansı | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | &lt;tag=Magic&gt; Kritik Hasarı | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | &lt;tag=Magic_NoIcon&gt; Hasarı | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | &lt;tag=Magic_NoIcon&gt; Savunmayı Yok Sayma | 1 = 1% |
| `MAX_HP` | Max HP | Maks. HP | Tam sayı |
| `MAX_MP` | Max MP | Maks. MP | Tam sayı |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt; Kapasitesi | Tam sayı |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | &lt;tag=DarkCloud&gt; Kapasitesi | 1 = 1% |
| `MOVE_SPEED` | Move Speed | Hareket Hızı | 1 = 1% |
| `MP_REGEN` | MP Regeneration | MP Yenilenmesi | Tam sayı |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | Öldürmede {VALUE} &lt;tag=MP&gt; Yenile | Tam sayı |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | &lt;tag=MP&gt; Yenileme Verimliliği | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | MP tüketen yeteneklerin hasarı | 1 = 1% |
| `MP_STEAL` | MP Steal | MP Çalma | Tam sayı |
| `NEGOTIATION` | Barter | Pazarlık | Tam sayı |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | Atılma Dokunulmazlığını Devre Dışı Bırak | Tam sayı |
| `NO_MAGIC_COST` | Grimoires do not consume MP | Büyü kitapları MP tüketmez | Tam sayı |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | Güçlendirme kullanırken 5 &lt;tag=Tiles&gt; içindeki oyunculara da güçlendirme sağlar. Ancak büyü maliyeti %50 artar. | Tam sayı |
| `PHYSICAL_DAMAGE` | Physical Damage | Fiziksel Hasar | Tam sayı |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | &lt;tag=PhysicalDamage&gt; Artışı | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | Gezegen Saldırı Hızı | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | Gezegen Hasarı | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | İksir Çantası Yuvası | Tam sayı |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | Alınan Hasar Azaltma | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | Başka bir oyuncuyu diriltme süresini {VALUE} azaltır | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | Safir Kazanım Bonusu | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | Özel Saldırı Hasarı | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | {VALUE} &lt;tag=Leaf:nocolor&gt; ile başla | Tam sayı |
| `SUB_BAG_SLOT` | Side Bag Slot | Yan Çanta Yuvası | Tam sayı |
| `THORNS` | Thorns | Dikenler | 1 = 1% |
| `TOUGHNESS` | Toughness | Dayanıklılık | Tam sayı |
| `TRUE_DAMAGE` | True Damage | Gerçek Hasar | Tam sayı |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | Her fazla kombo için &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | &lt;tag=WeaponAction_DirectAttack&gt; Kritik Vuruş Şansı | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | Yakın Dövüş Menzili | 1 = 1% |
