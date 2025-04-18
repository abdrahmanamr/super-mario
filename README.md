سوبر ماريو برذرز (2D)

دي لعبة بلاتفورمر متطورة في Unity مقتبسة من سوبر ماريو برذرز الأصلية للـ NES. بتحكم في ماريو وانت ماشي في Mushroom Kingdom عشان تنقذ Princess Peach من Bowser. بتعدي مستويات سايد-سكريولينج وتتجنب الأعداء والهادرز وتجمع القوة عن طريق Super Mushroom وFire Flower وStarman.

المواضيع

فيزياء (Physics)

**تصميم المستويات (Level Design)**n- إحساس اللعبة (Game Feel)

الإصدار

Unity 2021.3 (LTS)

روابط

تحميل الريبو: https://github.com/zigurous/unity-super-mario-tutorial/archive/refs/heads/main.zip

قائمة الفيديو: https://youtube.com/playlist?list=PLqlFiJjSZ2x1mrMpSQgYdRm8PyWRTg6He

التعديلات اللي عملناها

قائمة البداية (Start Menu)

أضفنا شاشة رئيسية فيها أزرار لاختيار المستوى (Level 1, Level 2, Level 3).

الانتقال التلقائي للمستوى التالي

عدلنا سكربت FlagPole واستخدمنا SceneManager.LoadScene عشان بعد ما تكمل مستوى ينتقل آليًا للمستوى اللي بعده بناءً على ترتيب المشاهد في Build Settings.

قائمة الخروج (Exit Menu)

أضفنا Exit Menu في آخر المشاهد، فيها زرين: “خروج من اللعبة” (Application.Quit أو إيقاف Play Mode في الـ Editor) و“عودة للقائمة الرئيسية”.

تنسيق الواجهة (UI Style)

استخدمنا Canvas وPanels منظمة باستخدام Layout Groups عشان الأزرار تبقى متراصة ومتساوية.

ضفنا سكربت ButtonScale عشان الأزرار تكبر شوية لما تمرر الماوس عليها، بتحسن تفاعل المستخدم.

استمتعوا باللعب، ولو احتجتوا أي تعديل أو إضافة، ابعتوا Pull Request 😉

