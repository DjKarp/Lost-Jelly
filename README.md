<p align="center">
  <img src="https://github.com/DjKarp/Lost-Jelly/blob/master/MainGit/A_2D_digital_illustration_promotional_banner_for_a.png" alt="Lost Jelly Banner" width="256" height="256"/>
</p>

<h1 align="center">Lost Jelly Remastered - Snake Puzzle</h1>

<p align="center">
  <b>Змейка, которой нужно не только двигаться, но и думать.</b><br/>
  Головоломка-аркада на Unity с уникальными уровнями, UI и реактивщиной.
</p>

<p align="center">
  <img src="https://github.com/DjKarp/Lost-Jelly/blob/master/MainGit/Blocks_01_256x256_Alt_02_006.png" alt="Lost Jelly Logo" width="100"/>
</p>

<p align="center">
  
- Платформы: Unity (URP)
- Жанр: Аркада + Головоломка
- Особенности: Ремастер уникальной игры с продуманными уровнями и новыми фичами
- Движок: Unity 2022+ (URP)
- Технологии: URP, 2D, Tileset, DoTween, R3, UnityAction, FMOD, мульти-язычная поддержка, редакторские скрипты
</p>

---

## 📁 Структура проекта
<pre> ```Assets/
├── Animation/          # Анимации из игры и видеороликов
├── Pictures            # Изображения для фона и лого
├── Prefab/
│   └── Audio/          # Audio manager 
│   └── Movie/          # Movie Controller
│   └── Resources/      # Prefabs для загрузки из кода - UIMain, Player, LocalizeManager, Levels Effect
│     └── Levels/       # Prefabs уровней, кладёшь сюда новые уровни, будут подкачиваться автоматически
│   └── UI/             # Prefabs меню и интерфейса: MainMenu, GamePlayUI, Input, LevelCard
├── Scenes/             # Все сцены игры 
│   ├── Bootstrap       # Разгоночная сцена, содержит только загрузочный экран, с неё запускаются все остальные сцены
│   ├── Movie           # Сцена на которой проигрываются видеоролики
│   ├── MainMenu        # Сцена для главного меню и сервисных окон
│   ├── Game            # Сцена на корторой создаётся игровое окружение и геймплей
├── Scripts/
│   ├── Audio/          # Audio manager и остальные скрипты для звука
│   ├── Control/        # Управление (джойстик, touch и др.)
│   ├── Effect/         # Графические эффекты уровней
│   ├── Game/           # Базовые архитектурные компоненты и Точки входа
│   ├── Level/          # Скрипты, принадлежащие уровням
│   ├── Localization/   # Перевод игры на разные языки, менеджер
│   ├── Movie/          # Контроллер видеороликов
│   ├── Player/         # Основные механики змейки
│   ├── UI/             # Интерфейсы, анимации
│   └── Util/           # Скрипты для редактора Unity и скрипты помощники
├──  Settings/          # Разнообразные настройки для плагинов и цвета градиентов
├──  StreamingAssets/   # FMOD банки, файлы локализации, DOTweenSettings
└── Sprite/             
│   ├── Hero/           # Nimble - голова змейки и ящики
│   ├── joystick/       # Touch control UI
│   ├── Level/          # TileAtlas для фона и дополнительные спрайты для объектов на уровнях
│   ├── Movie/          # Дым, земля и UFO из видеоролика
│   ├── Obstacles/      # Спрайты вызывающие конец игры при столкновении
│   ├── Resources/      # Загружаемые из кода картинки для эффектов на уровни и награды игроку
│   └── UI/             # Спрайты, UI  
``` </pre>
---

## 🎥 Старые скриншоты
<p align="center">
  <img src="https://img.itch.zone/aW1hZ2UvMzEwNjYvMTMxMTkwLnBuZw==/original/o5ZLT9.png" width="200"/>
  <img src="https://img.itch.zone/aW1hZ2UvMzEwNjYvMTMxMTg5LnBuZw==/original/4ysKSL.png" width="200"/>
  <img src="https://img.itch.zone/aW1hZ2UvMzEwNjYvMTMxMTk3LnBuZw==/original/TgySkm.png" width="200"/>
  <img src="https://img.itch.zone/aW1hZ2UvMzEwNjYvMTMxMjEwLnBuZw==/original/Asl3or.png" width="200"/>
</p>
## 🎥 Новые скриншоты
<p align="center">
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/MainMenu.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/MainMenu_Explore_LevelSelect.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/MainMenu_Explore_Settings.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/Gameplay_Level01.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/Gameplay_Level01_02.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/Gameplay_Level02.jpg" width="200"/>
  <img src="https://redleggames.com/images/Games/LostJellyRemaster/Gameplay_Level05.jpg" width="200"/>
</p>
---

## 🎥 Демонстрация

<p align="center">
<b>Смотреть видео на RuTube - Кликни по картинке</b><br/>
  </p>
<p align="center">
  
   [![Смотреть видео](https://redleggames.com/images/Games/LostJellyRemaster/Logo_FullHD_02.jpg)](https://rutube.ru/video/868f60dc7475d1665849b1e4b1f0920d/)
   
</p>

---

## 🎮 Об игре

> **Lost Jelly Remastered** — ремастер моей собственной игры. 
Это не просто аркадная змейка, а настоящая головоломка, где каждый уровень требует планирования. Ты не просто реагируешь — ты думаешь, выбираешь маршрут, экспериментируешь.  
Это гибрид аркады и головоломки, где каждый уровень был создан вручную и требует вдумчивого подхода. Игроку предстоит не только быстро реагировать, но и просчитывать ходы наперёд.

---

## ✨ Основные фичи игры

- 🧠 **Уникальные уровни**, созданные вручную
- Сложность основана на логике и стратегии, а не на случайности
- **Минималистичный, но стильный дизайн**

## ✨ Основные фичи проекта
- 📱 **Поддержка джойстика и тач-управления**
- 🌍 **Локализация на несколько языков**
- 🎨 **UI с анимациями DoTween**
- 🎮 **Визуал на URP**
- 🛠️ **Скрипты для редактора и создания уровней**
- 🛠️ **Лёгкая интеграция новых уровней**

---

## 🧪 Технологии

| Система | Использование |
|-----------------|---------------------------------------|
| `DoTween`       | UI анимации    |
| `UniRX\R3`      | Реактивное управление UI и логикой    |
| `UnityAction`   | Событийная система между компонентами |
| `URP`           | Графика и оптимизация                 |
| `Custom Editor` | Быстрое создание и редактирование уровней |

---

## 🌍 Локализация

Проект легко масштабируется: все переводы хранятся централизованно, подключение нового языка — дело пары минут.

---

## 🕹️ Управление

- **Мобильное:** кастомный джойстик
- **Клавиатура/ПК:** WASD / Стрелки
- **Геймпад:** полная поддержка

---

## 🧾 Как запустить
Скачай архив, распакуй и запусти LostJellyRemaster.exe:
https://redleggames.com/Games/LostJellyRemaster/Lost_Jelly_Remastered_v008.zip

Склонируй проект:
bash
git clone https://github.com/DjKarp/Lost-Jelly.git

Открыть в Unity 2022.3+ (URP)

Сцена запуска: любая - в Unity Editor нет разницы, какую запустить сцену - всё проинициализируется и загрузится с Boostrap сцены

Играй! 🎉

---

🛠️ **Планы на будущее**
- Больше интерактивных элементов
- Онлайн-система лидербордов
- Ачивки и выпуск в Steam, RuStore

Выполнено:
- Добавление новых головоломок (уровней)
- Ещё больше языков
