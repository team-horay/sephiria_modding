# รายการค่าสถานะ

ID ค่าสถานะที่ใช้ใน `stats` ของชุดแต่งกายและในโค้ดม็อด ค่าเป็นจำนวนเต็มในรูปแบบ `"ID/ค่า"` และคอลัมน์หน่วยแสดงวิธีแปลงค่าเมื่อแสดงผลในเกม ตัวอย่างเช่น การเขียน `CRITICAL/-400` สำหรับค่าสถานะที่ `100 = 1%` จะแสดงเป็น -4%

ตัวอย่างที่ชุดแต่งกายในเกมใช้จริง: `MOVE_SPEED/12`, `MAX_HP/-15`, `DEFENSE/10`, `EVASION/1200`, `CRITICAL/-400`, `ATTACK_SPEED/7`, `EXP_DROP/4`, `STARTING_MONEY/800`

| ID | ชื่อภาษาอังกฤษ | ชื่อ | หน่วย |
|---|---|---|---|
| `ATTACK_DASH` |  |  | 100 = 1 |
| `ATTACK_SPEED` | Attack Speed | ความเร็วโจมตี | 1 = 1% |
| `BASIC_ATTACK_DAMAGE` | Normal Attack Damage | ความเสียหายการโจมตีปกติ | 1 = 1% |
| `BOSS_REWARD_DICE` | Acquire {VALUE} dice upon defeating a boss | ได้รับลูกเต๋า {VALUE} ลูกเมื่อกำจัดบอส | จำนวนเต็ม |
| `BUFF_DURATION` | Duration of Buffs Applied by Self | ระยะเวลาบัฟที่ใช้กับตนเอง | 1 = 1% |
| `BURN_DAMAGE` | &lt;tag=Burn&gt; Damage | ความเสียหาย &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_DURATION` | &lt;tag=Burn&gt; duration | ระยะเวลา &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_SPEED` | Attack Speed of &lt;tag=Burn&gt; | ความเร็วโจมตีของ &lt;tag=Burn&gt; | 1 = 1% |
| `BURN_STACK` | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | &lt;tag=Burn&gt; &lt;tag=Stacks&gt; | จำนวนเต็ม |
| `CHARGING_CHARM_BONUS` | &lt;tag=FrostRelic&gt; Charge Speed | ความเร็วชาร์จ &lt;tag=FrostRelic&gt; | 1 = 1% |
| `CHARM_DAMAGE_BONUS` | &lt;tag=Artifact&gt; Damage | ความเสียหาย &lt;tag=Artifact&gt; | 1 = 1% |
| `COMBO_BONUS_DAMAGE` | &lt;tag=FinalDamage&gt; Bonus for each Activated Combo Type | โบนัส &lt;tag=FinalDamage&gt; ต่อประเภทคอมโบที่เปิดใช้งาน | 1 = 1% |
| `COOLDOWN_RECOVERY_SPEED` |  |  | จำนวนเต็ม |
| `CRITICAL` | Critical Chance | โอกาสคริติคอล | 100 = 1% |
| `CRITICAL_DAMAGE_RATE` | Critical Damage | ความเสียหายคริติคอล | 1 = 1% |
| `CROSSBOWAMMO` | Magazine Capacity | ความจุแม็กกาซีน | จำนวนเต็ม |
| `CROSSBOWFIRESPEED` | Crossbow Firing Speed | ความเร็วการยิงหน้าไม้ | 1 = 1% |
| `CROSSBOW_ADDITIONAL_MAGAZINE` | Magazine Count | จำนวนแม็กกาซีน | จำนวนเต็ม |
| `CROSSBOW_RELOADSPEED` | Crossbow Reload Speed | ความเร็วรีโหลดหน้าไม้ | จำนวนเต็ม |
| `DARK_CLOUD_DAMAGE` | Extra &lt;tag=DarkCloud&gt; damage | ความเสียหาย &lt;tag=DarkCloud&gt; เพิ่มเติม | 1 = 1% |
| `DARK_CLOUD_RESTORE_DURING_BATTLE` | &lt;tag=DarkCloud&gt; Recovery Rate during Combat | อัตราฟื้นฟู &lt;tag=DarkCloud&gt; ระหว่างการต่อสู้ | 1 = 1% |
| `DARK_CLOUD_SPEED` | Consumption Speed of &lt;tag=DarkCloud&gt; | ความเร็วการใช้ &lt;tag=DarkCloud&gt; | 1 = 1% |
| `DASH_ATTACK_DAMAGE` | Dash Attack Damage | ความเสียหายโจมตีพุ่ง | 1 = 1% |
| `DASH_ATTACK_IGNORE_DEFENSE` | Dash Attack Ignore Defense | เพิกเฉยต่อการป้องกันโจมตีพุ่ง | 1 = 1% |
| `DASH_COUNT` | Dash Count | จำนวนการพุ่ง | จำนวนเต็ม |
| `DASH_DISABLE` | Dash Disabled | ปิดใช้งาน &lt;tag=Dash&gt; | จำนวนเต็ม |
| `DASH_INVINCIBLE_TIME_BONUS` | Invincibility window bonus while dashing | โบนัสช่วงเวลาอมตะขณะพุ่ง | 1000 = 1s |
| `DASH_RECOVERY_SPEED` |  |  | 1 = 1% |
| `DASH_SPEED` | Dash speed | ความเร็วการพุ่ง | 1 = 1% |
| `DEBUFF_DAMAGE` | Damage dealt with &lt;tag=Debuff&gt; | ความเสียหายที่สร้างด้วย &lt;tag=Debuff&gt; | 1 = 1% |
| `DEBUFF_DURATION` | Duration of all debuffs inflicted on enemies | ระยะเวลาของดีบัฟทั้งหมดที่ทำให้ศัตรูติด | 1 = 1% |
| `DEFENSE` | Defense | พลังป้องกัน | จำนวนเต็ม |
| `DEFENSE_TO_ATTACK` | No damage reduction from &lt;tag=Defense&gt;, but increases damage dealt by increased &lt;tag=DamageReduction&gt; | ไม่ได้รับการลดความเสียหายจาก &lt;tag=Defense&gt; แต่เพิ่มความเสียหายที่สร้างตาม &lt;tag=DamageReduction&gt; ที่เพิ่มขึ้น | จำนวนเต็ม |
| `DIMENSION_POCKET` | &lt;tag=TEXT:DimensionPocket&gt; Capacity | ความจุ &lt;tag=TEXT:DimensionPocket&gt; | จำนวนเต็ม |
| `DIRECT_ATTACK_BURN` | {VALUE} chance to inflict &lt;tag=Burn&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | มีโอกาส {VALUE} ทำให้ติดดีบัฟ &lt;tag=Burn&gt; เมื่อใช้ &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DIRECT_ATTACK_ELECTRIC` | {VALUE} chance to inflict &lt;tag=Electric&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt;; | มีโอกาส {VALUE} ทำให้ติดดีบัฟ &lt;tag=Electric&gt; เมื่อใช้ &lt;tag=WeaponAction_DirectAttack&gt;; | 1 = 1% |
| `DIRECT_ATTACK_FROSTBITE` | {VALUE} chance to inflict &lt;tag=Frostbite&gt; debuff upon &lt;tag=WeaponAction_DirectAttack&gt; | มีโอกาส {VALUE} ทำให้ติดดีบัฟ &lt;tag=Frostbite&gt; เมื่อใช้ &lt;tag=WeaponAction_DirectAttack&gt; | 1 = 1% |
| `DISABLE_DUAL_ARTIFACT` | &lt;tag=TEXT:BondArtifact&gt; no longer appears | &lt;tag=TEXT:BondArtifact&gt; จะไม่ปรากฏอีกต่อไป | จำนวนเต็ม |
| `ELECTRIC_DAMAGE` | &lt;tag=Electric&gt; extra damage | ความเสียหายเพิ่มเติมจาก &lt;tag=Electric&gt; | 1 = 1% |
| `ELECTRIC_STACK` | &lt;tag=Electric&gt; &lt;tag=Stacks&gt; | จำนวน &lt;tag=Stacks&gt; ของ &lt;tag=Electric&gt; | จำนวนเต็ม |
| `ELITE_DAMAGE` | Bonus damage dealt to &lt;tag=EnemyType_Boss&gt; and &lt;tag=EnemyType_Miniboss&gt; | ความเสียหายเพิ่มเติมต่อ &lt;tag=EnemyType_Boss&gt; และ &lt;tag=EnemyType_Miniboss&gt; | 1 = 1% |
| `EVASION` | Evasion | หลบหลีก | 100 = 1 |
| `EVASION_DISABLE` | &lt;tag=Evasion&gt; Disabled | ปิดใช้งาน &lt;tag=Evasion&gt; | 1 = 1% |
| `EVASION_RATE_REDUCTION` | Evasion Chance Reduction | ลดโอกาสหลบหลีก | 1 = 1% |
| `EXP_DROP` | Experience Drop | ประสบการณ์ที่ดรอป | 1 = 1% |
| `EXTRA_ITEM_CHOICES` | Additional Item Choices | ตัวเลือกไอเทมเพิ่มเติม | จำนวนเต็ม |
| `EXTRA_LIFE` | Revive with 60% HP upon death ({VALUE} times) | ฟื้นคืนชีพด้วย HP 60% เมื่อเสียชีวิต ({VALUE} ครั้ง) | จำนวนเต็ม |
| `EXTRA_MIRACLE_CHOICES` | Additional Miracle Choices | ตัวเลือกปาฏิหาริย์เพิ่มเติม | จำนวนเต็ม |
| `EXTRA_WEAPON_CHOICES` | Additional Weapon Choices | ตัวเลือกอาวุธเพิ่มเติม | จำนวนเต็ม |
| `FINAL_DAMAGE` |  |  | 1 = 1% |
| `FINAL_HP` | Final HP | HP สุดท้าย | 1 = 1% |
| `FINAL_MP` | Final MP | MP สุดท้าย | 1 = 1% |
| `FINAL_WEAPONDAMAGE` | &lt;tag=Weapon&gt; Damage | ความเสียหาย &lt;tag=Weapon&gt; | 1 = 1% |
| `FIRE_DAMAGE` | Fire Damage | ความเสียหายไฟ | จำนวนเต็ม |
| `FIRE_DAMAGE_AMP` | &lt;tag=FireDamage&gt; Amplification | การขยาย &lt;tag=FireDamage&gt; | 1 = 1% |
| `FIXED_DASH` | Dash count fixed at {VALUE} | จำนวนการพุ่งถูกกำหนดไว้ที่ {VALUE} | จำนวนเต็ม |
| `FLAME_SWORD_ADDITIONAL_ATTACK` | Additionally activates &lt;tag=FlameSword&gt; {VALUE} times | เปิดใช้งาน &lt;tag=FlameSword&gt; เพิ่มเติม {VALUE} ครั้ง | จำนวนเต็ม |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_MAGIC` | When dealing &lt;tag=Magic:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | เมื่อสร้างความเสียหาย &lt;tag=Magic:noicon&gt; จะเปิดใช้งาน &lt;tag=FlameSword&gt; เพิ่มเติม {VALUE} ครั้ง | จำนวนเต็ม |
| `FLAME_SWORD_ADDITIONAL_ATTACK_FROM_WEAPON` | When dealing &lt;tag=Weapon:noicon&gt; damage, additionally activates &lt;tag=FlameSword&gt; {VALUE} times | เมื่อสร้างความเสียหาย &lt;tag=Weapon:noicon&gt; จะเปิดใช้งาน &lt;tag=FlameSword&gt; เพิ่มเติม {VALUE} ครั้ง | จำนวนเต็ม |
| `FLAME_SWORD_COOLDOWN` | &lt;tag=FlameSword&gt; Cooldown | คูลดาวน์ &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_CRITICAL` | Critical Chance of &lt;tag=FlameSword&gt; | โอกาสคริติคอลของ &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_DAMAGE` | &lt;tag=FlameSword&gt; Damage | ความเสียหายของ &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_IGNORE_DEFENSE` | Ignore target's defense by {VALUE} with &lt;tag=FlameSword&gt; | เพิกเฉยต่อพลังป้องกันของเป้าหมาย {VALUE} ด้วย &lt;tag=FlameSword&gt; | 1 = 1% |
| `FLAME_SWORD_LUCK` | &lt;tag=FlameSword&gt; has a {VALUE} chance to grow in size and increase damage by &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% when thrown | เมื่อขว้าง &lt;tag=FlameSword&gt; มีโอกาส {VALUE} ที่จะขยายขนาดและเพิ่มความเสียหาย &lt;tag=CONST:flameSwordLuckBonusDamagePercent&gt;% | 1 = 1% |
| `FLAME_SWORD_MAX` | &lt;tag=FlameSword&gt; Maximum Count | จำนวนสูงสุดของ &lt;tag=FlameSword&gt; | จำนวนเต็ม |
| `FOLLOWER_ATTACK_SPEED` | Companion Attack Speed | ความเร็วโจมตีของสหาย | 1 = 1% |
| `FOLLOWER_CRITICAL` | Companion Critical Chance | โอกาสคริติคอลของสหาย | 100 = 1% |
| `FOLLOWER_DAMAGE` | Companion Damage | ความเสียหายของสหาย | 1 = 1% |
| `FOLLOWER_DEFENSE` | Companion Defense | พลังป้องกันของสหาย | จำนวนเต็ม |
| `FOLLOWER_MAX_HP` | Companion Max HP | HP สูงสุดของสหาย | 1 = 1% |
| `FOLLOWER_REVIVE` | Companion Revive Time Haste | เร่งเวลาชุบชีวิตสหาย | 1 = 1% |
| `FREEZE_DAMAGE` | &lt;tag=Freeze&gt; Debuff Damage | ความเสียหายดีบัฟ &lt;tag=Freeze&gt; | 1 = 1% |
| `FROSTBITE_DAMAGE` | Targets affected by &lt;tag=Frostbite&gt; take damage per second equal to {VALUE}% of &lt;tag=IceDamage&gt; (damage increases with &lt;tag=Stacks&gt;) | เป้าหมายที่ได้รับผลจาก &lt;tag=Frostbite&gt; จะได้รับความเสียหายต่อวินาทีเท่ากับ {VALUE}% ของ &lt;tag=IceDamage&gt; (ความเสียหายเพิ่มตาม &lt;tag=Stacks&gt;) | จำนวนเต็ม |
| `FROST_RELIC_CRITICAL_DAMAGE` | &lt;tag=FrostRelic&gt; critical damage | ความเสียหายคริติคอลของ &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FROST_RELIC_DAMAGE` | &lt;tag=FrostRelic&gt; damage | ความเสียหายของ &lt;tag=FrostRelic&gt; | 1 = 1% |
| `FRUIT_COUNT` | Fruit Skewer Max | จำนวนสูงสุดของผลไม้เสียบไม้ | จำนวนเต็ม |
| `FURY_INVINCIBLE_TIME` | Invincibility Duration During &lt;tag=WeaponAction_Fury&gt; | ระยะเวลาอมตะระหว่าง &lt;tag=WeaponAction_Fury&gt; | 10 = 1s |
| `HEALING_PENALTY` | Healing Curse: Healing received decreases by {VALUE} | คำสาปการรักษา: การรักษาที่ได้รับลดลง {VALUE} | 1 = 1% |
| `HIGHEST_ELEMENTAL_DAMAGE` | Highest Elemental Damage | ความเสียหายธาตุสูงสุด | จำนวนเต็ม |
| `HP_CURSE` | Max HP set to {VALUE} | กำหนด HP สูงสุดเป็น {VALUE} | จำนวนเต็ม |
| `HP_POTION_BONUS` | HP Potion Bonus | โบนัสโพชั่น HP | 100 = 1% |
| `HP_STEAL` | Life Steal | ดูดเลือด | จำนวนเต็ม |
| `ICE_DAMAGE` | Cold Damage | ความเสียหายความเย็น | จำนวนเต็ม |
| `ICE_DAMAGE_AMP` | &lt;tag=IceDamage&gt; Amplification | เพิ่มความเสียหาย &lt;tag=IceDamage&gt; | 1 = 1% |
| `IGNORE_DEFENSE` | Ignore Defense | เพิกเฉยต่อการป้องกัน | 1 = 1% |
| `IGNORE_SUPER_ARMOR` | Extra damage to target’s &lt;tag=SuperArmor&gt; | ความเสียหายเพิ่มเติมต่อ &lt;tag=SuperArmor&gt; ของเป้าหมาย | 1 = 1% |
| `INFINITY_DASH` | Infinity Dash | พุ่งได้ไม่จำกัด | จำนวนเต็ม |
| `INFINITY_MP` | Magic Reflow: &lt;tag=MaxMP&gt; is fixed at &lt;tag=CONST:infinityMPMax&gt;, and MP does not decrease | การไหลเวียนเวท: &lt;tag=MaxMP&gt; ถูกกำหนดเป็น &lt;tag=CONST:infinityMPMax&gt; และ MP จะไม่ลดลง | จำนวนเต็ม |
| `INVENTORY_SLOT` | Inventory Slot | ช่องเก็บของ | จำนวนเต็ม |
| `LEAF_DROP` | Leaf Drop | ใบไม้ร่วง | 1 = 1% |
| `LEVEL_UP_HEAL` | HP Restored on Level Up | ฟื้นฟู HP เมื่อเลเวลอัป | 1 = 1% |
| `LIGHTNING_DAMAGE` | Lightning Damage | ความเสียหายสายฟ้า | จำนวนเต็ม |
| `LIGHTNING_DAMAGE_AMP` | &lt;tag=LightningDamage&gt; Amplification | เพิ่มความเสียหาย &lt;tag=LightningDamage&gt; | 1 = 1% |
| `LUCK` | Luck | โชค | จำนวนเต็ม |
| `MAGIC_COST_REDUCE` | Reduce MP Cost of &lt;tag=Magic:noicon&gt; | ลดค่า MP ของ &lt;tag=Magic:noicon&gt; | 1 = 1% |
| `MAGIC_CRITICAL` | &lt;tag=Magic&gt; Critical Chance | โอกาสคริติคอล &lt;tag=Magic&gt; | 100 = 1% |
| `MAGIC_CRITICAL_DAMAGE_RATE` | &lt;tag=Magic&gt; Critical Damage | ความเสียหายคริติคอล &lt;tag=Magic&gt; | 1 = 1% |
| `MAGIC_DAMAGE_BONUS` | &lt;tag=Magic_NoIcon&gt; Damage | ความเสียหาย &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAGIC_IGNORE_DEFENSE` | &lt;tag=Magic_NoIcon&gt; Ignore Defense | เพิกเฉยต่อการป้องกัน &lt;tag=Magic_NoIcon&gt; | 1 = 1% |
| `MAX_HP` | Max HP | HP สูงสุด | จำนวนเต็ม |
| `MAX_MP` | Max MP | MP สูงสุด | จำนวนเต็ม |
| `MIN_DARK_CLOUD` | &lt;tag=DarkCloud&gt; Capacity | ความจุ &lt;tag=DarkCloud&gt; | จำนวนเต็ม |
| `MIN_DARK_CLOUD_AMP` | &lt;tag=DarkCloud&gt; Capacity | ความจุ &lt;tag=DarkCloud&gt; | 1 = 1% |
| `MOVE_SPEED` | Move Speed | ความเร็วเคลื่อนที่ | 1 = 1% |
| `MP_REGEN` | MP Regeneration | การฟื้นฟู MP | จำนวนเต็ม |
| `MP_REGEN_KILL` | Recover &lt;tag=MP&gt; {VALUE} on Kill | ฟื้นฟู &lt;tag=MP&gt; {VALUE} เมื่อสังหาร | จำนวนเต็ม |
| `MP_REGEN_MULTIPLE` | Efficiency When Recovering &lt;tag=MP&gt; | ประสิทธิภาพเมื่อฟื้นฟู &lt;tag=MP&gt; | 1 = 1% |
| `MP_SKILL_DAMAGE` | Damage of abilities consuming MP | ความเสียหายของสกิลที่ใช้ MP | 1 = 1% |
| `MP_STEAL` | MP Steal | ดูด MP | จำนวนเต็ม |
| `NEGOTIATION` | Barter | ต่อรอง | จำนวนเต็ม |
| `NO_DASH_INVINCIBLE` | Disable Dash Invincibility | ปิดใช้งานอมตะขณะพุ่ง | จำนวนเต็ม |
| `NO_MAGIC_COST` | Grimoires do not consume MP | คัมภีร์ไม่ใช้ MP | จำนวนเต็ม |
| `PARTY_BUFF` | Grants buffs to players within 5 &lt;tag=Tiles&gt; when casting buffs. However, the spell cost is increased by 50%. | เมื่อร่ายบัฟ จะมอบบัฟแก่ผู้เล่นในระยะ 5 &lt;tag=Tiles&gt; อย่างไรก็ตาม ค่าร่ายเวทจะเพิ่มขึ้น 50% | จำนวนเต็ม |
| `PHYSICAL_DAMAGE` | Physical Damage | ความเสียหายกายภาพ | จำนวนเต็ม |
| `PHYSICAL_DAMAGE_AMP` | &lt;tag=PhysicalDamage&gt; Amplification | เพิ่มความเสียหาย &lt;tag=PhysicalDamage&gt; | 1 = 1% |
| `PLANET_ATTACK_SPEED` | Planet Attack Speed | ความเร็วโจมตีดาวเคราะห์ | 1 = 1% |
| `PLANET_DAMAGE` | Planet Damage | ความเสียหายดาวเคราะห์ | 1 = 1% |
| `POTION_SLOT` | Potion Bag Slot | ช่องกระเป๋าโพชั่น | จำนวนเต็ม |
| `RECEIVED_DAMAGE_REDUCTION` | Damage Taken Reduction | ลดความเสียหายที่ได้รับ | 1 = 1% |
| `REVIVE_PLAYER_HASTE` | Reduces the time to revive another player by {VALUE} | ลดเวลาชุบชีวิตผู้เล่นคนอื่นลง {VALUE} | 1 = 1% |
| `SAPPHIREBONUS` | Sapphire Acquisition Bonus | โบนัสการได้รับแซฟไฟร์ | 1 = 1% |
| `SPECIAL_ATTACK_DAMAGE` | Special Attack Damage | ความเสียหายโจมตีพิเศษ | 1 = 1% |
| `STARTING_MONEY` | Start with {VALUE} &lt;tag=Leaf:nocolor&gt; | เมื่อเริ่มการสำรวจ {VALUE} &lt;tag=Leaf:nocolor&gt; | จำนวนเต็ม |
| `SUB_BAG_SLOT` | Side Bag Slot | ช่องกระเป๋าข้าง | จำนวนเต็ม |
| `THORNS` | Thorns | หนาม | 1 = 1% |
| `TOUGHNESS` | Toughness | ความทรหด | จำนวนเต็ม |
| `TRUE_DAMAGE` | True Damage | ความเสียหายจริง | จำนวนเต็ม |
| `UNLIMITED_COMBO` | For each excess Combo, &lt;tag=FinalDamage&gt; | ต่อคอมโบส่วนเกิน &lt;tag=FinalDamage&gt; | 1 = 1% |
| `WEAPON_CRITICAL` | Critical Hit Chance of &lt;tag=WeaponAction_DirectAttack&gt; | โอกาสคริติคอลของ &lt;tag=WeaponAction_DirectAttack&gt; | 100 = 1% |
| `WEAPON_RANGE` | Melee Attack Range | ระยะโจมตีระยะประชิด | 1 = 1% |
