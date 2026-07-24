# نظام إدارة المكتبة (Library Management System)

مشروع تطبيق وحدة تحكم (Console Application) مبني بلغة **C#** باستخدام إطار العمل **Entity Framework Core (Code-First Approach)** وقاعدة بيانات **Microsoft SQL Server**. يهدف المشروع إلى إدارة المكتبات بشكل احترافي من خلال تنظيم الكتب، المؤلفين، الأعضاء المسجلين، وسجلات الاستعارة والإرجاع.

---

## 🛠️ التقنيات المستخدمة
* **Language:** C# (.NET)
* **ORM:** Entity Framework Core (Code-First)
* **Database:** Microsoft SQL Server / LocalDB
* **IDE:** Visual Studio

---

## 🏗️ هيكلية المشروع (Project Architecture)
تم تنظيم المشروع بأسلوب هندسي واضح يفصل نماذج البيانات 
:(Models) عن طبقة الاتصال وقاعدة البيانات ومنطق التشغيل

```text
LibraryMangmentEF1/
│
├── Models/                  # مجلد نماذج البيانات (Tables)
│   ├── Author.cs            # كلاس المؤلف
│   ├── Book.cs              # كلاس الكتاب
│   ├── Member.cs            # كلاس العضو
│   └── BorrowRecord.cs      # كلاس سجل الاستعارة
│
├── Migrations/              # ملفات ترحيل قاعدة البيانات (EF Migrations)
├── LibraryContext.cs        # كلاس السياق والاتصال بقاعدة البيانات (DbContext)
├── SeedData.cs              # كلاس تهيئة وإدخال البيانات الافتراضية
└── Program.cs               # منطق البرنامج الرئيسي وقائمة الخيارات التفاعلية
