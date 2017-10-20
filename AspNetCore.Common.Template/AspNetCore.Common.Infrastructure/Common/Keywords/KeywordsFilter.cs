﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AspNetCore.Common.Infrastructure.Common
{
    public class KeywordsFilter
    {
        private const string KEY_WORDS = @"15768|32283|40698|40698|71776|985985|。gm|@sshole|∪R|∪R靠|┻┼|?|02jam|1000y|13點|168www|１６大|17game|17Game|17爱网|18dy|18禁|18摸|１８摸|18淫书|21ｃｎ|22park|2逼靠|306医院|365?sex|365情色|39CK|39仓Ku|3p炮图|3P炮图|3P炮圖|50zhou|51vip|51yxb|58jb|5jq|5kuai|5街区|6。4|６。４|6?4靠|64yl|64动乱|64動亂|64事件|68wow|7。22|777黄站|77bbb|7hero|8?仙|8?仙靠|8。9|8?9靠|8？9靠|88jb|89-64cdjp|89暴乱|89暴亂|89动乱|89動亂|89风波|89風波|89六四|89学潮|89學潮|89运动|8仙|8仙靠|933cn|97sese|988wow|98bb|99bb|99BBS|9JBW|9v9e|9城|9之遊|9之游|a$$hole|a$shole|a4u|a4y|abc?abc|ADMIN|Admin|admin|admin靠|adult|aids|Aids|AIDS|aika|ai滋|ａｉ滋|ALERT|Alod|amateur|anal|apesoft|apex|apexon|Apexsoft|appie|Archlord|as$hole|asgard|asiasex|asktao|ASS|asshole|aszol|avatar|av成人|av贴图|av貼圖|ayawawa|AYAWAWA|a片|A片|ａ片|Baichi|baichi|bankai|Baopi|Bao皮|Bastard|bastard|BASTARD|basterd|batard|bbscity|Biaozi|BIAOZI|biaozi|Biao子|bignews|bingnews|Bitch|bitch|BITCH|Bi样|ｂｉ样|bjzc|blogbaby|BLOW|blowjobs|BlowJobs|blueeye|BnB|bo?ke|boke|bong|boxilai|boxun|bt成人|BT激情|BT淫穴|bukake|bukakke|bukkake|butthead|butthole|bxqt|B博士|b毛|B毛|b样|B样|ｂ样|c?a?o|c?a?o靠|cabal|camon|cao|CAO?NI?MA|cao?ni?ma|caoB|caobi|cao靠|cao你|cctv|CCTV|cdjp|changeu|chao?nv|cha你|chenxun|chinamz|chuan?qi|chui?chui|ci77|cjsh|CM|cnd|CNN|cnouc|com流氓|connard|conquer|counselor|creaders|create|CREATE|cronous|ctracer|cunt|cunt?cunt|d?f?d?z|d?p?p|d7se|da?hua|da?tang|dafa|dajiyuan|dalai|damm|damn|dao?jian|defan|defannet|dekaron|delete|DELETE|dfdz|dfjoy|dh315|dick|Dick|DICK|DJMAX|dou?dou|dragon|droiyan|DROP|dtfy|dyonline|earthciv|eight仙|enculer|epochtime|eqsf|everstar|ezgaming|Ezgaming|f?a?l?u?n|f?a?轮|f?a?輪|f?l?g|f?u?c?k|F.L.G|F。L。G|F_U_C_K|f_u_c_k|Fag|falu|FALUN|falun|falundafa|FALUNDAFA|fa轮|felch|feltch|feng?shen|feng?yun|fgmtv|Fku|fku|FLG|flg|flyfff|fofg|fosaon|foseaon|foutre|freechina|freedom|freenet|fu(|fuc|FUCK|Fuck|fuck|fuck?fuck|Fuck?You|fuck?you|FUCK?YOU|FUCKYOU|fuckyou|fuck靠|fuck骚|fuck傻B|fuck傻逼|fuck售ID|fuck死gd|fuck死GD|fuck死gm|fuck死GM|fuk|G?m|G?M|g?m|g。m|G。M|g8|game|game17|game588|gamegold|gameline|gamemaste|GAMEMASTE|GameMaste|gamemy|GAN|gangbang|gan你|gaowan|gay|GC365|gc365|GCD|gcd|ggol|ghost|GM|ＧＭ|Ｇｍ|ｇｍ|G-M|GM001|GMworker|gmworkers|GN|gong?fu|granado|Groove|GY|ＧＹ|ｇｙ|g点|g片|ha?bao|habbo|hanbit|hanbiton|hanxiang|hardcore|hdw|helbreath|hellgate|helper|hero108|herogame|heting|heyong|hkhk68|hong?yue|hongzhi|hotsex|hrichina|hua?xia|huanet|hui?huang|hujintao|H动漫|h动漫|H動漫|h站|h站靠|i3hun|icpcn|incest|insert|itembay|iuiuu|ｊ8|j8靠|Jap?Jap|JB|jb|ｊｂ|jb靠|JB靠|jhsz|JHSZ|ji?zhan|jiabao|jianghu|jiangshan|jian你|jiaochun|Jiaochun|jing?ling|jinku|jintao|jinv|jinyong|Ji女|ｊｉ女|JJ|joyxy|JPEEN|ju?shang|jushang|jx2|jy2|kai?tian|kai?xuan|kaixuan|kakajb|kang?zhan|Kao|karma|kart|ke?luo?si|KEFU|Kefu|KeFu|kele8|kjking|ｋｋ粉|kqking|kuaik|Kurumi|K粉|ｋ粉|K姐|K他命|ladeng|laghaim|laqia|lastchaos|Lateinos|lihongzhi|like999|lineage2|ling?tu|lipeng|liuqi|LIUSI|liuxiao|long?hun|long?zu|lovebox|luanshi|luo?qi|mabinogi|MAD?MAD|madelove|MADELOVE|makai|MAKELOVE|makelove|Maki|making|manager|mankind|mannweib|market|MASTER|master|mbs|meimei穴|meinv|meinv穴|merde|meretriz|metin|mforest|mi?zhuan|mierda|ming?yun|minghui|minhui|mir|mir3|mixmaster|MK?星云|mland|MM屄|mm屄|mm美图|mm美圖|MM嫩穴|mo?jian|mo?xiang|mo?yu|moxiang|moyu|muhon|mwo|mxd|mystina|mythos|nabi|nacb|nage|naive|navyfield|NeoSteam|neosteam|ＮＥＴ|ｎｅｔ|netbar|netdream|NEWSPACE|nmis|nnd|nnd=|NPC靠|nude?nude|o2jam|Obama|Offgamers|offgamers|olgad|On?Air|ON9|on9|onair|onewg|onhave|operator|orgasmus|orgasums|Paki?Paki|pangya|pao?pao|paper64|partita|Party|pcik|peacehall|peachall|penis|PENIS|pet520|petgirl|petrealm|phuc|phuck|piao?piao|piss|pk1937|playboy|pnisse|polla|Poon?Poon|popkart|popoming|PORN|porn|project|pussy|PUSSY|qeeloo|qi?ji|qi?shi|qi?xia|qi?yu|qian?nian|qiangjian|qqr2|qqtang|QQtang|Qqyinsu|QQ幻想|ｑｑ堂|QQ音速|Q币|Rape|rape|raycity|ray-city|redmoon|renewal|renminbao|repent|ri|RI?NI?MA|rivals|rivals靠|rjwg|roi?world|roiworld|rong?yao|rplan|runstar|rx008|rxjh|rxjhhvip|rxjhvip|rxjhwg|rxwg|s_b|safeweb|saga|salop|san?guo|sanguohx|saobi|SARS|sars|Sb|SB|sb|ｓｂ|screw|sega|segame|server|service|sex|ｓｅｘ|Sex?Sex|sf|sh!t|shemale|shen?hua|shen?qi|shengda|Shine|shine|shit|SHIT|Shit|Shit?Shit|shizhang|shyt|silkroad|simple|slanglist|SM|sm調教|sm女王|SM女王|SM舔穴|sm舔穴|sm调教|SM援交|sm援交|snatch|soma|space|sperm|sphincter|suck|SUCK|Suck|svdc|sw2|swdol|System|system|SYSTEM|T.M.D|T.M.D靠|T。M。D|t2dk|TABLE|Taiwan国|tampon|tantra|taobao|taobao靠|teen|teen?sexy|teensexy|TENGREN|TENGWU|TeSt|test|TEsT|tEST|tESt|testicle|thsale|tian?shi|tianji|tianjing|tiao?zhan|tibet|tibetalk|Tibet国|tmd|Tmd|TMD|ｔｍｄ|TMD靠|TNND|tnnd|to173|TOM在线|tr|trannie|tranny|travesti|triangle|Trickster|tta2|ttee|tth2|Ttmd|TTMD|ttwg666|tum?tum|TW|tw18|u???r靠|U???R靠|u??r靠|U??R靠|u?r|U?R|U?R靠|u?r靠|U/R|U/R靠|UltraSurf|unixbox|UPDATE|UR|ur|ＵＲ|U-R|urban|urban靠|urTNND|ur靠|UR靠|ＵＲ靠|U-R靠|ustibet|vaameline|vgbh|viprxjh|voa|Voyage|vrtank|VULVA|Waigua|wangce|wangyang|wangyou99|wanwang|webmaster|WEB牌戰|WEB战牌|wetback|wg17173|wg2222|wg666|wg8800|wg886|wg9996|wgpj|WG武功|whore|whoyo|wow520|wow711|wowassist|wowbank|wowcaifu|wowjingye|wstaiji|wtf|wu?hun|wyd|xi?you|xiao77|xinsheng|xizang|xucaihou|xyq|yeeplay|YGBH|ygbh|yifeng|yong?heng|youxia|YOUXIKA|youxika|yuanming|yuckfou|yuckfu|yuetao|yuming|yutong|yxzbw|zeming|zhengjian|zhengqing|zhuanfalu|zhuxi|zirui|zjdfjoy|zlywy|zongli|z毛二逼|ㄐ八|ㄐ巴|ㄐ掰|ㄖ|阿扁|阿拉|阿沛|阿片烟|啊无卵|哀悼|挨球|艾滋|艾滋病|爱色cc|爱液|爱幼阁|爱滋|愛女人|愛色cc|愛液|愛幼閣|愛滋|安拉|安理会|安眠酮|安纳咖|安南|按摩棒|傲神傳|傲视|奥巴马|奥马尔|奥运|奧運|八?仙|八嘎|八九|八老|八仙|八仙靠|扒屄|扒光|扒穴|拔屄|拔出来|罢餐|罢吃|罢饭|罢工|罢食|掰穴|白痴|白癡|白粉|白烂|白立忱|白立朴|白莲教|白小姐|百海|百家乐|班禅|包pi|包二奶|包皮|薄码|薄碼|薄一波|宝德|保钓|保釣|保监会|保密局|鸨|鲍戈|鲍彤|鲍威尔|鲍鱼|鲍岳桥|暴动|暴動|暴干|暴幹|暴奸|暴乱|暴亂|暴乳|暴徒|暴淫|鮑彤|鮑嶽橋|爆吧|爆操|爆草|爆乳|爆乳娘|贝肉|貝肉|被操|被插|被干|被幹|本拉丹|苯比|苯丙胺|笨屄|笨逼|笨比|屄|屄屄|屄毛|逼奸|逼毛|逼你老母|逼痒|逼癢|逼样|逼樣|逼孕|逼孕套|比的|比卵|比毛|比水|比样|碧香亭|避孕|避孕套|鞭神|鞭王|变态|婊|婊zi|婊子|婊子靠|瘪三|癟三|宾周|賓周|冰毒|冰锋|波霸|波霸?|勃起|博白县|博讯|博訊|不举|不玩了|布莱尔|布雷尔|布雷爾|布什|布希|步飞烟|步非烟|财政部|采花堂|採花堂|彩票机|菜B|菜b|菜逼|参事室|残废|殘廢|殘废|殘疾|沧澜曲|蔵獨|藏独|藏独靠|藏獨|藏妇会|藏婦會|藏青会|藏青會|藏青社|操|操?gm|操78|操Gm|操GM|操gm|操gM|操meimei|操mm穴|操XX|操屄|操逼|操逼毛|操逼样|操比|操比毛|操表姐|操婊|操婊子|操宾周|操冰毒|操波霸|操博讯|操布什|操藏独|操操|操操gM|操操gm|操操Gm|操操GM|操操XX|操操逼|操操比|操操蛋|操操你|操操他|操草妈|操肏|操插gm|操插gM|操插Gm|操插GM|操插你|操插妳|操朝鲜|操车臣|操车仑|操陈功|操陈毅|操吃屎|操赤匪|操抽插|操吹箫|操春药|操蠢猪|操戳你|操达赖|操打炮|操大B|操大逼|操大便|操大法|操蛋|操狗|操黑|操护士|操机掰|操鸡|操雞|操烂|操爛|操林|操妹妹|操母狗|操嫩穴|操尼|操你|操你爸|操你妈|操你妈屄|操你媽|操你娘|操妳|操妳妈|操妳媽|操妳娘|操妻|操嫂嫂|操射|操神|操爽|操死|操他|操他妈|操她|操她妈|操王|操我|操小穴|操穴|操肿|操腫|曹刚川|曹剛川|曹建明|曹你|曹庆泽|曹长青|草的你妈|草妈|草拟妈|草你|草你妈|草你媽|草你娘|草擬媽|草嫣|草芝麻|草枝麻|肏|肏屄|肏穴|冊老|冊那|册老|册那|厕奴|侧那|测绘局|测拿|测试員|测試员|廁奴|測试員|測試员|測試員|曾道人|曾培炎|曾庆红|曾庆紅|插?10p|插?11p|插?12p|插?13p|插?14p|插?15p|插?3p|插?4p|插?5p|插?6p|插?7p|插?8p|插?9p|插B|插b|插gm|插gM|插GM|插Gm|插暴|插逼|插比|插后庭|插後庭|插进|插進|插嫩逼|插嫩穴|插你|插你爸|插你妈|插你媽|插你娘|插妳|插屁眼|插深些|插死你|插他|插她|插我|插小穴|插穴|插穴图|插阴|插阴茎|插陰|插淫屄|柴玲|蝉童|产权局|產黨|猖妓|娼|娼妇|娼妓|長瀨|常劲|常委|倡议书|朝天穴|朝鲜|朝鮮|潮吹|潮喷|潮噴|车臣|车仑|車侖|陈炳基|陈伯达|陈独秀|陈功|陈良|陈良宇|陈蒙|陈丕显|陈破空|陈水扁|陈随便|陈希同|陈晓宁|陈宣良|陈一谘|陈毅|陈永贵|陈至立|陈总统|陳水扁|陳希同|陳曉寧|陳毅|陳雲|陳宗興|陳總統|成克杰|成人BT|成人bt|成人片|程凯|程真|吃大便|吃鸡巴|吃雞巴|吃精|吃屎|痴乳|癡乳|迟浩田|遲浩田|赤匪|赤化|赤裸|抽插|抽动肉|抽你丫的|臭b|臭化西|臭机|臭机八|臭鸡巴|臭西|臭西化|臭作|处女膜|処女|豖者|畜生|處女|传奇3G|传销|傳奇3G|傳銷|创网|吹喇叭|吹萧|吹箫|吹簫|锤锤|锤子|春药|春藥|蠢猪|戳B|戳b|戳逼|戳比|戳你|慈善会|慈善會|次郎|崔英杰|催情药|错B|错b|错逼|错比|錯B|錯比|达赖|达賴|达癞|達赖|達賴|打飞机|打炮|打砲|打手枪|打野炮|打砸抢|大B|大b|大j8|大逼|大比|大便|大波|大波波|大参考|大东亚|大東亞|大法|大花B|大花逼|大会堂|大鸡巴|大雞巴|大纪元|大纪园|大紀元|大揭露|大力丸|大卵泡|大卵子|大乱交|大亂交|大妈油|大麻|大麻油|大奶头|大肉棒|大乳|大乳頭|大史|大史记|大史纪|大使官|大卫教|大衛教|大血B|大血比|大煙|大阳具|大跃进|呆比|呆卵|代挂|代炼|代链|代練|带练|带炼|带链|帶练|帶炼|帶链|戴秉國|戴海静|戴红|戴晶|戴维教|弹?劾|弹劾|彈?劾|档案局|党|党委|党卫兵|党中央|党主席|黨中央|荡妇|荡妹|荡女|蕩婦|蕩妹|蕩女|刀OL|刀online|刀剑|倒台|倒臺|盗撮|盗电|盗窃犯|道教|盜撮|盜竊犯|登?辉|登?輝|登辉|邓发|邓力群|邓小平|邓笑贫|邓颖超|鄧朴方|鄧小平|鄧穎超|迪凡|地震局|蒂巴因|电监会|叼你|叼你妈|叼你媽|屌|屌7|屌鸠|屌毛|屌妳|屌七|屌西|吊子|钓鱼岛|钓鱼台|釣魚島|丁关根|丁關根|丁元|丁子霖|丁字裤|腚眼|东社|东升|东条|东突|东亚|东洋屄|东正教|東Mai骚|東突|東洋屄|董必武|董建华|董建華|董贱华|董文华|懂文华|动乱|胴体|動乱|動亂|都江堰|豆豆秀|毒贩|毒販|毒品|独夫|独立|獨裁|赌马|赌球|杜冷丁|杜鲁门|杜智富|渡口|短信猫|断电|断水|斷电|斷電|斷水|多人轮|多人輪|多维|多维社|多维网|多維社|多維網|堕淫|墮淫|屙|屙民|俄罗斯|饿b|饿B|饿比|餓比|恩格斯|二B|二b|二屄|二逼|发$$抡|发$$仑|发$$伦|发$$沦|发$$纶|发$$轮|发$$论|发$抡|发$仑|发$伦|发$沦|发$纶|发$轮|发$论|发**抡|发**仑|发**伦|发**沦|发**纶|发**轮|发**论|发*抡|发*仑|发*伦|发*沦|发*纶|发*轮|发*论|发@@抡|发@@仑|发@@伦|发@@沦|发@@纶|发@@轮|发@@论|发@抡|发@仑|发@伦|发@沦|发@纶|发@轮|发@论|发^^抡|发^^仑|发^^伦|发^^沦|发^^纶|发^^轮|发^^论|发^抡|发^仑|发^伦|发^沦|发^纶|发^轮|发^论|发~~抡|发~~仑|发~~伦|发~~沦|发~~纶|发~~轮|发~~论|发~抡|发~仑|发~伦|发~沦|发~纶|发~轮|发~论|发改委|发浪|发愣|发抡|发抡功|发仑|发伦|发伦功|发沦|发纶|发轮|发论|发论公|发论功|发骚|发正念|発妻|発射|發$$掄|發$$倫|發$$綸|發$$論|發$侖|發$淪|發$輪|發**掄|發**倫|發**綸|發**論|發*侖|發*淪|發*輪|發@@掄|發@@倫|發@@綸|發@@論|發@侖|發@淪|發@輪|發^^掄|發^^倫|發^^綸|發^^論|發^掄|發^侖|發^倫|發^綸|發^輪|發^論|發~~掄|發~~侖|發~~倫|發~~淪|發~~綸|發~~輪|發~~論|發~掄|發~侖|發~倫|發~淪|發~綸|發~輪|發~論|發浪|發掄|發掄功|發侖|發倫|發倫功|發淪|發綸|發輪|發論功|廢墟|法$$抡|法$$掄|法$$仑|法$$伦|法$$沦|法$$纶|法$$轮|法$$侖|法$$倫|法$$淪|法$$綸|法$$輪|法$$论|法$$論|法$抡|法$掄|法$仑|法$伦|法$沦|法$纶|法$轮|法$侖|法$倫|法$淪|法$綸|法$輪|法$论|法$論|法*|法**抡|法**掄|法**仑|法**伦|法**沦|法**纶|法**轮|法**侖|法**倫|法**淪|法**綸|法**輪|法**论|法**論|法*功|法*抡|法*掄|法*仑|法*伦|法*沦|法*纶|法*轮|法*侖|法*倫|法*淪|法*綸|法*輪|法*论|法*論|法@@抡|法@@掄|法@@仑|法@@伦|法@@沦|法@@纶|法@@轮|法@@侖|法@@倫|法@@淪|法@@綸|法@@輪|法@@论|法@@論|法@抡|法@掄|法@仑|法@伦|法@沦|法@纶|法@轮|法@侖|法@倫|法@淪|法@綸|法@輪|法@论|法@論|法^^抡|法^^掄|法^^仑|法^^伦|法^^沦|法^^纶|法^^轮|法^^侖|法^^倫|法^^淪|法^^綸|法^^輪|法^^论|法^^論|法^抡|法^掄|法^仑|法^伦|法^沦|法^纶|法^轮|法^侖|法^倫|法^淪|法^綸|法^輪|法^论|法^論|法~~抡|法~~掄|法~~仑|法~~伦|法~~沦|法~~纶|法~~轮|法~~侖|法~~倫|法~~淪|法~~綸|法~~輪|法~~论|法~~論|法~抡|法~掄|法~仑|法~伦|法~沦|法~纶|法~轮|法~侖|法~倫|法~淪|法~綸|法~輪|法~论|法~論|法lun功|法功|法国|法愣|法抡|法抡功|法掄|法仑|法仑工|法仑公|法仑功|法仑攻|法仑共|法伦|法伦功|法囵功|法沦|法沦功|法纶|法轮|法轮工|法轮公|法轮功|法轮攻|法轮共|法侖|法侖功|法倫|法陯功|法菕功|法崘功|法淪|法惀功|法婨功|法棆功|法腀功|法碖功|法耣功|法蜦功|法綸|法輪|法輪功|法踚功|法磮功|法錀功|法鯩功|法稐功|法论|法论工|法论公|法论功|法论攻|法论共|法埨功|法溣功|法論|法黁功|法西斯|法谪|法谪功|法制办|反党|反黨|反动|反動|反封锁|反革命|反攻|反共|反华|反人类|反人民|反日|反社会|反社會|反政府|犯践|犯賤|犯踐|方励之|方晓日|方毅|方舟子|房事|放荡|放蕩|放尿|放屁|非典|扉之阴|扉之陰|肥逼|肥西|废墟|分家在|分裂|焚烧|粉屄|粉红穴|粉紅穴|粉穴|粪便|糞|糞便|风尘劫|风艳阁|封杀|封神榜|冯东海|冯素英|佛教|佛祖|夫妻3p|服务器|福呵定|付申奇|复辟|傅杰|傅鹏|傅全有|傅申奇|傅铁山|傅志寰|傅作义|傅作義|干bi|干gM|干gm|干GM|干Gm|干ＧＭ|干X|干x娘|干X娘|干爆|干逼|干比|干到|干的你|干的爽|干干|干机|干机掰|干鸡|干啦|干勒|干拎娘|干林|干尼|干你|干你良|干你妈|干你娘|干妳|干妳妈|干妳马|干妳娘|干娘|干您|干您娘|干炮|干七八|干汝|干入|干骚女|干嫂子|干爽|干死|干死CS|干死GM|干死你|干他|干他妈|干它|干她|干她妈|干牠|干我|干穴|干一干|干一家|幹|幹bi|幹GM|幹ＧＭ|幹x娘|幹逼|幹比|幹的你|幹的爽|幹機掰|幹拎娘|幹你|幹你良|幹你媽|幹你娘|幹妳|幹妳媽|幹妳馬|幹妳娘|幹您娘|幹炮|幹砲|幹七八|幹全家|幹死|幹死CS|幹死GM|幹死你|幹他|幹她|幹穴|幹一家|赣您娘|贛您娘|灨你娘|冈峦|刚比|刚瘪三|刚度|肛|肛jiao|肛屄|肛交|肛门|肛門|岡巒|钢管舞|剛比|剛癟三|剛度|鋼管舞|港澳办|高级逼|高級逼|高俊|高丽棒|高丽朴|高麗棒|高麗朴|高麗樸|高莺莺|高治联|高自联|睪丸|睾|睾丸|膏药旗|膏藥旗|搞B|搞b|搞比|搞你|搞他|搞她|戈万钧|戈扬|哥精|歌华|革命|格老子|个批|個批|给你爽|給你爽|工力|工商局|工自联|工作員|弓虽|弓雖|公安部|公安局|公务员|公媳乱|公媳亂|公子冲|公子开|功法|龚学平|龚學平|龔学平|龔學平|共*党|共产|共产党|共铲党|共產黨|共鏟黨|共黨|共匪|共狗|共军|共軍|共荣圈|共榮圈|狗B|狗b|狗比|狗操|狗成|狗诚|狗城|狗乘|狗干|狗幹|狗卵|狗卵子|狗娘|狗屁|狗日|狗日的|狗日靠|狗剩|狗屎|狗养|狗養|狗杂种|狗雜種|購金|孤儿|孤兒|古柯|古龙|谷牧|顾顺章|瓜批|瓜婆娘|瓜娃子|挂机|掛機|拐卖|关卓中|观世音|觀世音|管里|管里员|管理员|管理員|管理者|管裏員|光线|光線|广电|广电局|广闻|龟儿子|龟公|龟毛|龟奴|龟孙子|龟头|龟投|龟頭|亀頭|龜兒子|龜公|龜奴|龜头|龜投|龜頭|鬼村|鬼公|鬼轮奸|鬼輪奸|鬼輪姦|滚滚球|滚那吗|滾那嗎|郭?平|郭伯雄|郭罗基|郭平|郭岩华|国?贼|国安局|国防部|国管局|国民党|国务院|国研|国研室|国贼|国资委|國?賊|國產AV|國軍|國民黨|國權|國務院|國研|國賊|哈宝|哈批|骸卒|海盗王|海盜王|海乐神|海洛因|海明威|海洋局|駭客|酣乐欣|含屌|含鳥|韩光|韩桂芝|韩联潮|韩启德|韩天石|韩正|韩杼滨|韓啟德|韓正|汉?奸|汉奸|汉娃娃|漢?奸|漢娃娃|行房|豪乳|好嫩|好朋友|好色cc|郝建秀|何德普|何候华|何候華|何加栋|何勇|河?殇|河?殤|河殇|核潜艇|核武器|贺国强|贺龙|賀國強|賀龍|黑鲍娇|黑鮑嬌|黑屄|黑逼|黑鬼|黑毛屄|黑社会|黑手党|黑手黨|黑天使|黑五类|红宝石|红灯区|红海湾|红卫兵|红月|宏?法|宏法|宏象|洪?传|洪?傳|洪?吟|洪传|洪兴|洪興|洪吟|洪志|紅燈區|紅衛兵|紅月|后庭|後庭|胡XX|胡嘉|葫芦案|蝴蝶逼|虎门|虎门靠|虎骑|虎騎|互淫|护士穴|花蕊|华盛顿|华义|哗哗曼|華國鋒|華義|嘩嘩曼|欢城|欢乐城|歡樂城|换妻|換妻|皇军|皇軍|黄　菊|黄??菊|黄片|黄色|黄色靠|黃片|囘良玉|回回|回教|回良玉|回民|回民暴|悔过书|汇款|匯款|昏药|婚外恋|婚外戀|混沌|混沌诀|混沌靠|火棒|火麻|獲救|机八|机巴|机叭|机吧|机掰|机战|鸡8|鸡八|鸡巴|鸡巴靠|鸡叭|鸡芭|鸡吧|鸡掰|鸡店|鸡鸡|鸡奸|鸡女|鸡歪|鸡院|积克馆|姬胜德|基督|基督教|機八|機巴|機吧|機戰|激插|激情MM|激情色|激情淫|雞8|雞八|雞巴|雞叭|雞芭|雞吧|雞掰|雞雞|雞奸|雞女|雞歪|雞院|鷄巴|极景|極景|集体淫|集體淫|几八|几巴|几叭|几芭|幾八|幾巴|幾叭|幾芭|擠母奶|计生委|纪登奎|妓|妓女|妓女靠|妓院|家乐福|嘉联|贾庆林|贾廷安|贾育台|假钞|賈慶林|奸|奸暴|奸你|奸情|奸染|奸他|奸她|奸污|奸一奸|奸淫|奸幼|姦|姦情|姦染|姦淫|姦汙|监察部|监管|监听器|监听王|检察院|建国党|贱B|贱b|贱bi|贱逼|贱比|贱货|贱人|贱种|剑网|剑网2|剑网3|賤|賤B|賤bi|賤逼|賤比|賤貨|賤人|賤種|劍網|江Core|江core|江ze民|江八|江八点|江八条|江八條|江独裁|江獨裁|江核心|江流氓|江罗|江氏|江戏子|江贼|江贼民|江賊民|江折民|江猪|江猪媳|江豬|江豬媳|江主席|将则民|僵贼|僵贼民|僵賊民|薑春雲|疆独|疆獨|讲法|降半旗|酱猪媳|醬豬媳|交媾|交通部|姣西|脚交|腳交|叫床|叫春|叫鸡|叫雞|叫小姐|教派|教徒|教养院|教育部|揭批书|劫机|金币网|金伯帆|金酷|金毛穴|金瓶梅|紧穴|緊穴|劲爆|劲乐|劲舞团|劲樂|勁暴|勁爆|勁乐|勁樂|禁书|经血|經血|精蟲|精水|精童|精液|精液浴|精子|警奴|靖国|静坐|纠察员|鸠|鸠屎|糾察員|九城|九霾|九评|九評|久遊|久遊網|久游|久游网|久之遊|救災|就去日|菊花洞|菊花蕾|巨屌|巨奶|巨乳|巨骚|巨騷|聚丰|军妓|军委|军转|軍妓|卡弗蒂|开苞|开发|开房|开天|開苞|開天|凯丰|看牌器|看棋器|看中国|康生|抗日|抗曰|尻|尻庇|靠|靠爸|靠北|靠背|靠么|靠母|靠你妈|靠你媽|靠夭|靠腰|嗑药|磕药|磕藥|可待因|可卡叶|可卡葉|可卡因|可可精|客报|客服|掯|孔雀王|抠穴|摳穴|口爆|口合|口活|口交|口交靠|口肯|口射|口淫|寇晓伟|哭么|哭夭|裤袜|褲襪|垮台|垮臺|快感|快克|快樂AV|狂操|狂插|葵|坤邁|拉案);|拉丹|拉登|拉凳|拉客|拉皮条|拉皮條|拉手冲|喇嘛|来插我|来爽我|赖昌星|賴昌星|瀨名|拦截器|览叫|懒8|懒八|懒叫|懒教|懶8|懶八|懶叫|懶教|懶趴|烂b|烂B|烂屄|烂逼|烂比|烂屌|烂货|烂鸟|烂人|烂游戏|滥B|滥逼|滥比|滥货|滥交|濫B|濫逼|濫比|濫貨|濫交|爛B|爛逼|爛比|爛貨|狼友|浪妇|浪婦|浪叫|浪女|浪穴|劳教|老b|老B|老鸨|老逼|老比|老瘪三|老癟三|老江|老卵|老毛|老毛子|老母|老骚比|老骚货|老騷比|老騷貨|老味|黎安友|黎阳评|礼品|礼品机|李?录|李?禄|李?祿|李?錄|厉无畏|例假|厲無畏|麗春苑|连邦|连胜德|连线机|莲花逼|連戰|联?总|联大|联合国|联梦|联易|联众|蓮花逼|聯?總|聯眾|炼功|两国论|兩國論|亮屄|亮穴|淋病|灵游记|凌辱|靈遊記|领导|流氓|流蜜汁|流淫|流淫水|劉傑|劉淇|六.四|六。四|六?四|六？四|六合采|六合彩|六四|六-四|龙虎|龙虎豹|龙虎靠|龙新民|龍陽|娄义|婁義|漏逼|卢福坦|卢跃刚|陆定一|陆肆|陆委会|陸肆|路易|露B|露b|露屄|露逼|露点|露點|露毛|露乳|露穴|露阴照|露陰照|卵子|乱交|乱伦|亂交|亂倫|抡功|掄功|仑功|伦功|沦功|纶功|轮暴|轮操|轮大|轮干|轮公|轮功|轮攻|轮奸|轮流干|轮盘赌|轮盘机|轮子功|侖功|倫功|淪|淪功|耣|綸功|輪暴|輪公|輪功|輪攻|輪奸|輪姦|輪子功|罗　干|罗??干|骡干|羅幹|騾幹|裸聊|裸陪|躶|洛奇|旅游局|氯胺酮|妈b|妈B|妈逼|妈逼靠|妈比|妈的|妈的b|妈的B|妈的靠|妈个b|妈个B|妈个比|妈妈的|妈批|妈祖|媽B|媽逼|媽比|媽的|媽的B|媽個B|媽個比|媽媽的|媽祖|麻痹|麻黄素|麻黃素|麻醉枪|麻醉药|嗎b|嗎逼|嗎比|嗎的|嗎啡|嗎個|玛雅网|瑪雅網|鰢|吗b|吗逼|吗比|吗的|吗的靠|吗啡|吗啡碱|吗啡片|吗个|买财富|买春|买春堂|買幣|買財富|買春|買賣|買月卡|麦角酸|麦叫酸|売春婦|卖.国|卖。国|卖b|卖B|卖ID|卖QQ|卖逼|卖比|卖财富|卖国|卖号|卖号靠|卖卡|卖软件|卖骚|卖淫|賣B|賣ID|賣逼|賣比|賣幣|賣財富|賣國|賣號|賣軟體|賣騷|賣淫|賣月卡|馒头屄|瞒报|满洲国|滿洲國|曼德拉|蔓ぺ|猫扑|貓撲|毛XX|毛鲍|毛鮑|毛厕洞|毛廁洞|毛独立|毛二B|毛二屄|毛二逼|毛发抡|毛发伦|毛发轮|毛发论|毛发骚|毛法功|毛法愣|毛法仑|毛法轮|毛反动|毛反共|毛反华|毛反日|毛佛教|毛佛祖|毛傅鹏|毛干gm|毛干gM|毛干GM|毛干Gm|毛干你|毛干妳|毛肛|毛肛交|毛肛门|毛高俊|毛睾|毛睾丸|毛工力|毛公安|毛共匪|毛共狗|毛狗b|毛狗操|毛狗卵|毛狗娘|毛狗屁|毛狗日|毛狗屎|毛狗养|毛龟公|毛龟头|毛鬼村|毛滚|毛哈批|毛贺龙|毛洪兴|毛洪志|毛后庭|毛胡XX|毛花柳|毛欢城|毛换妻|毛黄菊|毛回回|毛回教|毛昏药|毛火棒|毛机八|毛机巴|毛鸡|毛鸡八|毛鸡巴|毛鸡叭|毛鸡芭|毛鸡掰|毛鸡鸡|毛鸡奸|毛基督|毛妓|毛妓女|毛妓院|毛奸|毛奸你|毛奸淫|毛贱|毛贱逼|毛贱货|毛贱人|毛江八|毛江青|毛江猪|毛疆独|毛姣西|毛叫床|毛叫鸡|毛禁书|毛精液|毛精子|毛静坐|毛鸠|毛鸠屎|毛军妓|毛军委|毛抗日|毛尻|毛靠|毛靠腰|毛客服|毛口交|毛狂操|毛拉登|毛懒教|毛烂B|毛烂屄|毛烂逼|毛烂比|毛烂屌|毛烂货|毛老逼|毛老母|毛李鹏|毛李山|毛连战|毛联大|毛联易|毛列宁|毛林彪|毛刘军|毛刘淇|毛流氓|毛六四|毛卵|毛轮功|毛轮奸|毛罗干|毛骡干|毛妈B|毛妈逼|毛妈比|毛妈的|毛妈批|毛妈祖|毛吗啡|毛卖B|毛卖ID|毛卖QQ|毛卖逼|毛卖比|毛卖国|毛卖号|毛卖卡|毛卖淫|毛毛XX|毛美国|毛蒙独|毛迷药|毛密洞|毛密宗|毛民运|毛奶子|毛嫩b|毛嫩B|毛伱妈|毛你爸|毛你姥|毛你妈|毛你娘|毛鸟gM|毛鸟gm|毛鸟GM|毛鸟Gm|毛鸟你|毛牛逼|毛牛比|毛虐待|毛喷你|毛彭真|毛皮条|毛屁眼|毛片|毛嫖客|毛破坏|毛破鞋|毛仆街|毛普京|毛强奸|毛强卫|毛抢劫|毛乔石|毛侨办|毛切七|毛情色|毛去死|毛人大|毛人弹|毛人民|毛日Gm|毛日GM|毛日gm|毛日gM|毛日你|毛肉棒|毛肉壁|毛肉洞|毛肉缝|毛肉棍|毛肉穴|毛乳|毛乳房|毛乳交|毛乳头|毛撒尿|毛塞白|毛三陪|毛氵去|毛骚|毛骚B|毛骚逼|毛骚货|毛色情|毛傻B|毛傻屄|毛傻逼|毛傻比|毛傻吊|毛傻卵|毛傻子|毛煞逼|毛上你|毛上妳|毛射精|毛石进|毛食屎|毛是鸡|毛手淫|毛售ID|毛售号|毛私服|毛死gd|毛死GD|毛死gm|毛死GM|毛孙文|毛他爹|毛他妈|毛台办|毛台独|毛台联|毛太监|毛特码|毛天皇|毛舔西|毛推油|毛退党|毛外挂|毛万税|毛王刚|毛王昊|毛王震|毛网管|毛温馨|毛倭国|毛倭寇|毛我操|毛我干|毛我日|毛吴仪|毛系统|毛鲜族|毛小泉|毛小穴|毛邪教|毛新党|毛性爱|毛性交|毛性欲|毛学潮|毛血逼|毛丫的|毛鸦片|毛阳精|毛阳具|毛耶苏|毛耶稣|毛夜情|毛一鲜|毛伊朗|毛阴部|毛阴唇|毛阴道|毛阴蒂|毛阴核|毛阴户|毛阴茎|毛阴毛|毛阴水|毛淫|毛淫荡|毛淫秽|毛淫货|毛淫贱|毛淫叫|毛淫毛|毛淫靡|毛淫水|毛淫娃|毛淫欲|毛硬挺|毛游行|毛玉杵|毛泽东|毛泽東|毛澤东|毛澤東|毛贼东|毛賊東|毛主席|毛阝月|玫瑰园|梅花屄|梅花网|梅花網|美国佬|美國佬|美利坚|美女穴|美乳|美骚妇|美騷婦|美沙酮|美少妇|美少婦|美穴|美幼|妹的穴|妹妹穴|蒙巴顿|蒙尘药|蒙独|蒙獨|蒙古独|猛插|猛干|梦遗|迷幻药|迷幻藥|迷魂药|迷奸|迷奸药|迷歼药|迷药|迷藥|米青|米田共|秘唇|秘裂|秘书长|密传|密洞|密碼|密穴|密宗|蜜洞|蜜穴|绵恒|喵的|灭绝罪|民國|民航局|民进党|民進黨|民运|民運|民阵|民政部|民政局|民猪|民主|民主潮|民主墙|民族|明慧|明慧網|摸咪咪|摸你|摸阴蒂|摸陰蒂|模拟机|摩力遊|摩力游|摩洛客|摩门教|摩門教|魔獸幣|莫伟强|墨香|默哀|谋杀|母奸|母親|穆斯林|那可汀|那妈|那媽|那嗎B|那嗎逼|那吗B|那吗逼|纳粹|納粹|奶娘|奶头|奶頭|奶罩|奶子|南联盟|南蛮子|南蠻子|脑残|嫐屄|闹事|內射|內衣|内测|内挂|内射|嫩b|嫩B|嫩BB|嫩bb|嫩鲍|嫩鲍鱼|嫩鮑|嫩鮑魚|嫩屄|嫩逼|嫩缝|嫩縫|嫩奶|嫩女|嫩穴|尼克松|倪志福|伱妈|你爸|你大爷|你大爺|你老妹|你老母|你老味|你姥|你姥姥|你妈|你妈逼|你妈比|你妈的|你妈靠|你媽|你媽逼|你媽比|你媽的|你马的|你馬的|你奶|你奶奶|你娘|你娘的|你娘咧|你全家|你色嗎|你是鸡|你是雞|你是鸭|你是鴨|你爷|你爺|你祖宗|妳妈的|妳媽的|妳马的|妳馬的|妳娘|妳娘的|捻|娘b|娘B|娘比|娘的|娘饿比|娘餓比|娘个比|娘個比|鸟g?M|鸟Gm|鸟GM|鸟gM|鸟gm|鸟gm?|鸟你|鳥g?M|鳥GM|捏弄|聶榮臻|宁王府|牛B|牛B靠|牛逼|牛逼靠|牛比|牛比靠|农业部|奴畜抄|奴事件|虐待|虐奴|诺亚|女屄|女尔|女干|女幹|女尻|女良|女马|女馬|女乃|女死囚|女也|女優|女友坊|拍卖官|潘国平|叛党|叛黨|叛国|叛國|膀胱|泡泡岛|炮友|喷　射|喷精|喷精?3p|喷你|喷尿|噴精|嘭嘭帮|嘭嘭幫|彭冲|彭德怀|彭德懷|彭佩云|彭珮云|彭珮雲|彭真|蓬浪|皮條|皮條客|屁蛋|屁股|屁精|屁眼|嫖|嫖娼|嫖客|姘|姘头|姘頭|品色堂|品香堂|品穴|平可夫|迫害|迫奸|破处|破處|破坏|破鞋|仆街|僕街|普京|普贤|萋|齐墨|骑你|骑他|骑她|起义|气象局|千年|前网|钱?达|钱达|錢?達|錢其琛|錢運錄|欠操|欠干|欠幹|欠骑|欠人骑|欠日|強暴|強姦|強姦犯|強姦你|強衛|强　奸|强暴|强奸|强奸犯|强奸你|强卫|抢火炬|抢劫|抢劫犯|抢粮记|抢尸|搶劫犯|窃听器|钦本立|亲?美|亲?日|亲美|亲民党|亲日|秦?晋|秦?晉|禽兽|禽獸|青楼|青樓|氢弹|情报|情報|情妇|情色|情色谷|情兽|情獸|庆?红|庆红|慶?紅|親?美|親?日|親民黨|親日|穷b|穷逼|邱会作|区委|去你的|去你妈|去妳的|去妳妈|去死|去他妈|去她妈|全裸|拳交|瘸腿帮|瘸腿幫|群p|群P|群奸|群交|群阴会|群陰會|然后|冉英|让你操|讓你操|热比娅|人大|人代|人代会|人弹|人民|人民報|人民币|人民幣|人妻|任弼时|任建新|任你淫|日b|日B|日Gm|日GM|日gM|日gm|日X?妈|日X?媽|日X妈|日啊|日本人|日屄|日逼|日比|日穿|日蛋|日翻|日九城|日军|日軍|日领馆|日你|日你爸|日你妈|日你媽|日你娘|日批|日爽|日死|日死你|日他|日他娘|日她|日王|鈤|荣毅仁|榮毅仁|柔阴术|肉棒|肉逼|肉壁|肉便器|肉唇|肉洞|肉缝|肉縫|肉沟|肉棍|肉棍子|肉壶|肉茎|肉莖|肉具|肉蒲团|肉蒲團|肉箫|肉簫|肉穴|肉欲|肉慾|乳霸|乳爆|乳房|乳峰|乳沟|乳溝|乳尖|乳交|乳尻|乳射|乳头|乳頭|乳腺|乳晕|乳暈|乳罩|润星|潤星|撒尿|撒泡尿|撒切尔|萨达姆|萨斯|塞白|塞你爸|塞你公|塞你母|塞你娘|赛你娘|赛妳娘|赛他娘|赛她娘|三K党|三K黨|三P|三p|三八淫|三挫仑|三国策|三级片|三級片|三角裤|三陪|三陪女|三唑仑|氵去|桑国卫|桑國衛|骚|骚b|骚B|骚B贱|骚棒|骚包|骚屄|骚屄儿|骚逼|骚比|骚洞|骚棍|骚货|骚鸡|骚姐姐|骚浪|骚卵|骚妈|骚妹|骚妹妹|骚母|骚女|骚批|骚妻|骚乳|骚水|骚穴|骚姨妈|懆您妈|懆您娘|騒|騷|騷B|騷B賤|騷棒|騷包|騷屄|騷逼|騷比|騷洞|騷棍|騷貨|騷雞|騷姐姐|騷浪|騷卵|騷媽|騷妹|騷妹妹|騷母|騷女|騷批|騷妻|騷乳|騷水|騷穴|騷姨媽|色97爱|色97愛|色成人|色弟弟|色电影|色鬼|色界|色空寺|色链|色猫|色貓|色咪咪|色迷城|色魔|色情|色情靠|色区|色區|色色|色色连|色书库|色图乡|色窝窝|色窩窩|色影院|色诱|色欲|色慾|杀人|杀人犯|傻×|傻b|傻B|傻B?|傻B靠|傻屄|傻逼|傻逼靠|傻比|傻吊|傻瓜|傻卵|傻鸟|傻鳥|傻批|傻子|煞逼|煞笔|煞笔靠|煞筆|山口組|删?号|删号|删号靠|伤亡|商务部|上访|上海帮|上你|上妳|少妇|少妇穴|少修正|邵家健|舌头穴|舌頭穴|社会院|社科院|射　精|射精|射了|射奶|射你|射屏|射爽|射颜|射顏|身寸|身障|神汉|神经病|神泪|神淚|神泣|神曲|沈彤|审计署|升达|升天|生春袋|生鸦片|生殖器|圣火|圣母|圣女峰|圣战|盛华仁|盛宣鸣|盛宣鳴|聖火|聖母|聖女峰|聖戰|尸虫|尸体|尸體|师春生|屍|屍体|屍體|師春生|湿了|湿穴|濕穴|十八代|十八摸|十景缎|十七大|十三点|十三點|石戈|石进|石首|食精|食捻屎|食屎|史迪威|史玉柱|驶你爸|驶你公|驶你母|驶你娘|屎你娘|屎妳娘|駛你爸|駛你公|駛你母|駛你娘|示威|世界都|世模|世维会|事屎|试看片|是鸡|是雞|释欲|釋欲|手淫|受精|受虐狂|受伤|受傷|受灾|受災|售ID|售号|售號|售软件|售軟體|兽奸|兽交|兽欲|獸奸|獸交|獸欲|熟妇|熟婦|熟母|熟女|数通|刷钱|双十节|爽你|爽图网|爽穴|水扁|水利部|税力|司法部|司马晋|司马璐|司徒华|丝袜|丝诱|私！服|私#服|私%服|私**服|私*服|私/服|私？服|私\\服|私\服|私￥服|私处|私處|私服|私-服|私—服|斯大林|絲襪|死gd|死GD|死GM|死gm|死全家|四川独|四清|四人帮|四人幫|四我周|宋xx|酥穴|酥痒|酥癢|他NND|他ㄇㄉ|他ㄇ的|他爸爸|他爹|他干|他妈|他妈的|他妈地|他妈靠|他媽|他媽的|他媽地|他嗎的|他马的|他馬的|他吗的|他母亲|他奶奶|他娘|他娘的|他祖宗|它NND|它爸爸|它妈|它妈的|它妈地|它媽的|它媽地|她NND|她爸爸|她妈|她妈的|她妈地|她妈靠|她媽的|她媽地|她马的|她馬的|她娘|塔利班|台办|台幣|台独|台独靠|台獨|台联|台聯|台聯黨|台盟|台湾|台湾党|台湾独|台湾狗|台湾国|台灣狗|台灣国|台灣國|台灣豬|臺|臺幣|臺獨|臺湾國|臺灣|臺灣黨|臺灣國|太监|太監|太子党|桃色|淘宝靠|套牌|套子|特码|特派员|腾人|腾武|滕人|滕仁|滕任|滕文生|滕武|藤人|藤仁|藤任|騰仁|騰武|騰訊|踢踢球|体奸|體奸|剃毛|天安门|天安門|天畅|天皇|天骄|天怒|天上碑|天堂2|天下贰|天下貳|天阉|天閹|天遊|天浴|天葬|天主教|天纵|天縱|甜嫩穴|舔?b|舔b|舔B|舔屄|舔逼|舔鸡巴|舔雞巴|舔脚|舔腳|舔奶|舔屁眼|舔西|调教|跳大神|铁道部|同床|同性恋|同性戀|童屹|统独|统战|统治|捅B|捅逼|捅比|捅你|捅死你|捅他|捅她|捅我|統治|痛经|偷欢|偷歡|偷拍|偷情|偷情網|凸点装|凸肉优|凸肉優|屠城|屠杀|推翻|推推侠|推推俠|推油|退党|退黨|退役|吞精|臀部|脫內褲|脫衣舞|脱内裤|脱衣舞|挖挂|瓦良格|歪逼|外　挂|外??挂|外$$挂|外$$掛|外$挂|外$掛|外**挂|外**掛|外*挂|外*掛|外/挂|外/掛|外?挂|外？挂|外？掛|外@@挂|外@@掛|外@挂|外@掛|外\\挂|外\挂|外\掛|外_挂|外_掛|外~~挂|外~~掛|外~挂|外~掛|外卦|外挂|外-挂|外—挂|外掛|外-掛|外—掛|外汇局|外交部|外阴|外陰|完蛋操|玩逼|玩穴|万淫堂|卐|萬鋼|萬人暴|萬稅|萬淫堂|王八|王八蛋|网?特|网爱|网捷信|网龙|網?特|網愛|網捷信|網龍|網星|網易|網域|威而钢|威而柔|维权|伟哥|尾行|猥亵|猥褻|卫生部|卫生巾|尉健行|慰安妇|慰安婦|慰春情|温B|温b|温逼|温比|溫B|溫逼|溫比|瘟B|瘟b|瘟比|文?革|文革|文九天|文物局|文胸|問道|倭国|倭寇|窝窝客|窩窩客|我操|我操靠|我操你|我草|我干|我幹|我和她|我奸|我就色|我考|我靠|我咧干|我日|我日靠|我日你|我有网|我周容|龌龊|齷齪|乌兰夫|无疆界|无界|无码|无码片|无毛穴|无网界|无修正|無界|無碼|無毛穴|無網界|無修正|五不|午夜场|西藏|西藏独|西藏国|西藏國|吸毒|吸毒犯|吸精|希拉克|希特勒|习近平|习仲勋|習近平|洗脑|洗脑班|洗腦班|洗钱|系統|狭义道|狹義道|下贱|下賤|下流|下三烂|下三滥|下三濫|下三爛|下身|下体|下體|下阴|下陰|下注|仙剑|仙劍|鲜族|鮮族|藓鲍|蘚鮑|现金|現貨|乡巴佬|相奸|想上你|向忠发|项怀诚|项小吉|嚮導|肖强|销魂洞|销售|銷魂洞|小B|小b|小B样|小B樣|小逼|小比样|小比樣|小瘪三|小癟三|小电影|小電影|小鸡巴|小鸡鸡|小雞巴|小雞雞|小灵通|小卵泡|小卵子|小嫩逼|小嫩鸡|小嫩雞|小平|小泉|小日本|小肉粒|小乳头|小乳頭|小骚逼|小骚比|小骚货|小騷比|小騷貨|小穴|小淫女|小淫穴|歇b|歇逼|邪教|写真|泄密|谢非|谢长廷|谢中之|謝長廷|褻|辛灏年|新党|新黨|新斷線|新幹線|新光明|新华社|新江湖|新疆独|新疆国|新疆國|新浪|新魔界|新生网|新生網|新石器|新四|新唐人|新闻办|新义安|新義安|新语丝|新郑和|新鄭和|鑫諾|信访局|信仰|邢铮|兴奋剂|幸存|性爱|性愛|性病|性高潮|性虎|性饥渴|性飢渴|性交|性交靠|性交课|性交課|性交图|性交圖|性奴|性奴会|性奴會|性虐|性虐待|性器|性情|性趣|性骚扰|性生活|性无能|性無能|性息|性佣|性傭|性欲|性之站|倖存|胸罩|熊炎|熊焱|羞耻母|羞恥母|轩辕二|轩辕剑|薛伟|穴海|穴爽|穴图|穴圖|穴淫|学潮|学联|学生妹|学习班|学运|学自联|學潮|學生妹|血逼|血比|血洒|血灑|血洗|寻仙|巡查员|巡查員|巡府媳|讯息|丫的|压制|押大|押小|鸦片|鸦片液|鸦片渣|鸭店|鴉片|壓迫|壓制|雅典娜|亚洲|亚洲色|亞情|亞無碼|亞洲色|烟草局|烟膏|煙膏|颜射|顏騎|顏射|嚴雋琪|艳乳|艳照|艳照门|艳照門|艷照门|艷照門|豔乳|豔照|豔照門|扬振|扬震|阳精|阳具|阳萎|阳痿|阳物|陽精|陽具|陽萎|陽痿|陽物|摇头丸|摇头玩|摇頭丸|搖头丸|搖頭丸|要色色|要射了|耶和华|耶苏|耶稣|耶蘇|野合|野鸡|野雞|夜情|夜色城|夜总会|夜總會|一本道|一党|一黨|一贯道|一貫道|一起玩|一四我|一夜欢|一夜歡|一夜情|伊拉克|伊朗|伊斯兰|依星|遗精|遗嘱|遗囑|遺囑|倚天二|义解|义母|亦凡|抑制剂|易当|易當|阴屄|阴部|阴唇|阴道|阴蒂|阴缔|阴阜|阴核|阴户|阴茎|阴莖|阴精|阴毛|阴门|阴囊|阴水|阴穴|陰屄|陰部|陰唇|陰道|陰蒂|陰締|陰阜|陰核|陰戶|陰茎|陰莖|陰精|陰毛|陰門|陰囊|陰水|隂|银民吧|淫|淫B|淫b|淫meimei|淫屄|淫屄儿|淫逼|淫痴|淫癡|淫虫|淫蟲|淫荡|淫蕩|淫电影|淫店|淫东方|淫洞|淫妇|淫婦|淫告白|淫棍|淫河|淫护士|淫秽|淫穢|淫货|淫貨|淫奸|淫间道|淫贱|淫賤|淫浆|淫漿|淫叫|淫浪|淫流|淫乱|淫亂|淫驴屯|淫驢屯|淫毛|淫妹|淫妹妹|淫糜|淫靡|淫蜜|淫民堂|淫魔|淫母|淫妞|淫奴|淫虐|淫女|淫女穴|淫妻|淫腔|淫情|淫色|淫少妇|淫湿|淫书|淫書|淫水|淫图|淫圖|淫娃|淫网|淫窝窝|淫西|淫穴|淫样|淫樣|淫液|淫欲|淫贼|淫汁|婬|滛|銀民吧|尹庆民|隐窝窝|隱窩窩|罂粟|罌粟|应招|应召|硬挺|應招|應召|悠遊網|悠游网|由喜贵|邮政局|铀|猶太豬|遊戲幣|游行|游衍|幼逼|幼齿|幼妓|幼交|幼男|幼女|幼图|幼圖|幼香阁|幼香閣|诱奸|诱色uu|誘姦|誘色uu|於天瑞|於永波|於幼軍|舆论|餘震|宇明网|雨星网|语句|玉杵|玉蒲团|玉蒲團|玉乳|玉穴|郁慕明|育碧|浴尿|预审查|欲火|欲女|慾|慾火|袁纯清|援交|援交妹|圓满|圓滿|远志明|曰GM|曰Gm|曰gM|曰gm|曰gＭ|曰本|曰你|月经|月經|运营人|运营长|运营者|运营组|运營者|运營组|运營組|運营者|運营組|運營者|運營组|運營組|杂种|雜種|再奸|昝爱宗|昝愛宗|早泄|早洩|造爱|造愛|造反|则民|择民|泽民|贼民|扎卡维|渣波波|战牌|战牌靠|招鸡|招雞|招妓|兆鸿|兆鴻|哲?民|哲民|贞操|针扎|貞操|真封神|真理教|真善忍|真主|姫辱|姫野爱|震级|震級|镇压|鎮壓|征途|正见网|正見網|正清网|正清網|正悟网|正悟網|证监会|郑源|政变|政變|政府|政权|政协|政協|政治|政治犯|政治局|鄭萬通|支那|知障|指导员|制服狩|质检局|致幻剂|致幻劑|智傲|智凡迪|智能H3|智障|中公网|中公網|中功|中共|中廣網|中国|中国猪|中國|中國狗|中國豬|中蕐|中华|中机电|中機電|中奖|中科院|中南海|中宣部|中央|重题工|诛仙|猪操|猪聋畸|猪猡|猪毛|猪毛1|猪容基|猪头|誅仙|豬操|豬容基|豬頭|主席|专政|专制|專政|專制|转法轮|轉法輪|装B|装B靠|装屄|装屄呢|装逼|装逼靠|装逼呢|卓奥|卓奧|子宫|子宮|梓霖|紫黛|自焚|自民党|自慰|自由门|宗教|总裁|总局|总理|总统号|總理|總書記|走光|走私|走资派|足脚交|足腳交|钻插|鑽插|阝月|作ai|作爱|作愛|作弊器|坐脸|坐台|坐台的|坐庄|做ai|做爱|做爱图|做愛|做雞|做鸭|做鴨";

        private HashSet<string> hash = new HashSet<string>();
        private byte[] fastCheck = new byte[char.MaxValue];
        private BitArray charCheck = new BitArray(char.MaxValue);
        private int maxWordLength = 0;
        private int minWordLength = int.MaxValue;
        private bool _isHave = false;
        private string _replaceString = "*";
        private char _splitString = '|';
        private string _newWord;

        /// <summary>
        /// 是否含有脏字
        /// </summary>
        public bool IsHave {
            get { return _isHave; }
        }

        /// <summary>
        /// 替换后字符串
        /// </summary>
        public string ReplaceString {
            set { _replaceString = value; }
        }

        /// <summary>
        /// 脏字字典切割符
        /// </summary>
        public char SplitString {
            set { _splitString = value; }
        }

        /// <summary>
        /// 更新后的字符串
        /// </summary>
        public string NewWord {
            get { return _newWord; }
        }

        public KeywordsFilter(string keywordsFilePath) {
            string keywords = string.Empty;
            if (File.Exists(keywordsFilePath)) {
                using (StreamReader sr = new StreamReader(File.OpenRead(keywordsFilePath), Encoding.UTF8)) {
                    keywords = sr.ReadToEnd();
                }
            }
            else {
                keywords = KEY_WORDS;
            }

            string[] badwords = keywords.Split(_splitString);
            foreach (string word in badwords) {
                maxWordLength = Math.Max(maxWordLength, word.Length);
                minWordLength = Math.Min(minWordLength, word.Length);
                for (int i = 0; i < 7 && i < word.Length; i++) {
                    fastCheck[word[i]] |= (byte)(1 << i);
                }

                for (int i = 7; i < word.Length; i++) {
                    fastCheck[word[i]] |= 0x80;
                }

                if (word.Length == 1) {
                    charCheck[word[0]] = true;
                }
                else {
                    hash.Add(word);
                }
            }
        }

        public bool HasBadWord(string text) {
            int index = 0;

            while (index < text.Length) {
                if ((fastCheck[text[index]] & 1) == 0) {
                    while (index < text.Length - 1 && (fastCheck[text[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (minWordLength == 1 && charCheck[text[index]]) {
                    return true;
                }

                //多字节检测
                for (int j = 1; j <= Math.Min(maxWordLength, text.Length - index - 1); j++) {
                    //快速排除
                    if ((fastCheck[text[index + j]] & (1 << Math.Min(j, 7))) == 0) {
                        break;
                    }

                    if (j + 1 >= minWordLength) {
                        string sub = text.Substring(index, j + 1);

                        if (hash.Contains(sub)) {
                            return true;
                        }
                    }
                }
                index++;
            }
            return false;
        }

        public string ReplaceBadWord(string text) {
            int index = 0;

            for (index = 0; index < text.Length; index++) {
                if ((fastCheck[text[index]] & 1) == 0) {
                    while (index < text.Length - 1 && (fastCheck[text[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (minWordLength == 1 && charCheck[text[index]]) {
                    _isHave = true;
                    text = text.Replace(text[index], _replaceString[0]);
                    continue;
                }
                //多字节检测
                for (int j = 1; j <= Math.Min(maxWordLength, text.Length - index - 1); j++) {
                    //快速排除
                    if ((fastCheck[text[index + j]] & (1 << Math.Min(j, 7))) == 0) {
                        break;
                    }

                    if (j + 1 >= minWordLength) {
                        string sub = text.Substring(index, j + 1);

                        if (hash.Contains(sub)) {
                            //替换字符操作
                            _isHave = true;
                            char cc = _replaceString[0];
                            string rp = _replaceString.PadRight((j + 1), cc);
                            text = text.Replace(sub, rp);
                            //记录新位置
                            index += j;
                            break;
                        }
                    }
                }
            }
            _newWord = text;
            return text;
        }
    }

    public static class StringEx
    {
        public static string ToFilterString(this string content) {
            KeywordsFilter keywordsFilter = new KeywordsFilter("keywords.txt");
            return keywordsFilter.ReplaceBadWord(content);
        }
    }
}