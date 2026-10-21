# 👻 Body Hop

> **La muerte no es el final. Salta entre los cuerpos de dos hermanos y rescata un alma del abismo antes de que se acabe el tiempo.**

**Body Hop** es un videojuego 3D de puzles y plataformas en primera persona desarrollado en **Unity** por **Dual Soul Games** para la asignatura de *Programación de Videojuegos*, basado en el tema de Game Jam *"La muerte no es el final"*.

[![Ver tutorial en YouTube](https://img.shields.io/badge/▶_Ver_tutorial-YouTube-red?style=for-the-badge&logo=youtube)](https://youtu.be/cs7xN2K4kso)
![Unity](https://img.shields.io/badge/Unity-3D-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Plataforma](https://img.shields.io/badge/Plataforma-PC-blue?style=for-the-badge&logo=windows)

---

## 📑 Índice

- [Historia](#-historia)
- [Concepto de juego](#-concepto-de-juego)
- [Mecánicas principales](#-mecánicas-principales)
- [Controles](#️-controles)
- [Escenarios y desafíos](#️-escenarios-y-desafíos)
- [Interfaz (HUD)](#️-interfaz-hud)
- [Diseño de arte y sonido](#-diseño-de-arte-y-sonido)
- [Programación](#-programación)
- [Estructura del proyecto](#-estructura-del-proyecto)
- [Assets de terceros](#-assets-de-terceros)
- [Uso de inteligencia artificial](#-uso-de-inteligencia-artificial)
- [Equipo](#-equipo)

---

## 📖 Historia

**Una odisea espiritual.** Tras un trágico accidente de coche en una carretera cualquiera, la vida de dos hermanos llega a su fin. En ese mismo instante, la **Muerte** se manifiesta y secuestra el alma de uno de ellos para confinarla para siempre en el abismo del inframundo.

**El objetivo.** El hermano restante debe superar los desafíos que impone la Muerte y rescatar el alma de su hermano, cooperando con su propio cuerpo inerte gracias a un lazo espiritual que trasciende la muerte.

### Personajes

- **🔴 Hermano Rojo / 🟢 Hermano Verde:** los dos recipientes espirituales del jugador. Comparten el lazo fraternal y las mecánicas de polaridad cromática.
- **💀 La Muerte:** la entidad antagonista y el catalizador del límite de tiempo.

---

## 🎮 Concepto de juego

Body Hop es un **"cooperativo para un solo jugador"**: no hay multijugador ni compañeros controlados por IA. Controlas a los dos hermanos tú mismo y avanzas de forma lineal por una serie de puzles lógicos y arquitectónicos interconectados.

⏱️ **Contrarreloj:** tienes **20 minutos** para completar el juego entero. Si el contador llega a cero, el alma del hermano se pierde y es *Game Over*.

> *Inspirado en puzles espaciales en primera persona como la saga **Portal**.*

---

## ✨ Mecánicas principales

### 🔄 Transición de alma
- **Exclusividad:** el alma solo puede controlar un cuerpo a la vez.
- **Estado inerte:** al cambiar de hermano, el cuerpo que abandonas se desploma y se queda tumbado e inmóvil donde estaba.
- **Autocooperación:** colocas a un hermano en un punto estratégico, cambias al otro y aprovechas lo que el primero ha alterado en el entorno.

### 🎨 Polaridad cromática
Ambos hermanos tienen las mismas capacidades físicas, pero una polaridad distinta: **rojo** y **verde**.
- Botones, palancas y puentes están hechos de uno de los dos materiales.
- Cada hermano **solo puede interactuar con objetos de su color y caminar sobre estructuras de su color**. Si el Hermano Verde pisa un puente rojo, lo atraviesa y cae al vacío, y viceversa.

---

## ⌨️ Controles

| Entrada | Acción | Comportamiento |
|---|---|---|
| `W` `A` `S` `D` | Movimiento | Desplazamiento del hermano activo |
| `Ctrl izquierdo` | Correr | Aumenta la velocidad de desplazamiento |
| `Espacio` | Saltar | Superar abismos y ganar altura |
| `E` | Interactuar | Activar palancas y pulsar botones cercanos |
| `Tab` | Transición de alma | Cambia el control al otro hermano, a cualquier distancia |
| `Ratón` | Mirar | Control de la cámara en primera persona |

---

## 🗺️ Escenarios y desafíos

El juego es lineal y se divide en dos escenarios con cuatro desafíos de dificultad progresiva.

### 🌳 Escenario 1: El Mundo Real (El Origen)
Representación realista del tramo de carretera donde ocurrió el accidente. Sirve como toma de contacto con los controles.

1. **El puente cromático y el laberinto.** Un río corta el paso. Un botón verde activa un puente rojo por el que solo cruza el Hermano Rojo. Tras resolver un laberinto, pulsa un botón rojo que vuelve el puente verde para que su hermano pueda reunirse con él.
2. **La apertura del portal al infierno.** En una gran explanada hay una palanca roja y otra verde. Al activar ambas con cada hermano, se abre un portal místico hacia el inframundo.

### 🔥 Escenario 2: El Infierno (El Abismo)
Un entorno de pesadilla con rocas gigantes y ríos de lava. Si caes, reapareces al inicio del puzle y pierdes tiempo.

3. **La cuadrícula invisible.** Sobre un mar de lava flota una cuadrícula de 3 × 9 plataformas, algunas falsas. Solo el Hermano Verde puede leer el cartel con la ruta segura, así que debes memorizarla y guiar al Hermano Rojo. Al otro lado, un segundo cartel indica el camino de vuelta para el Hermano Verde.
4. **Caminos cruzados y combinación secuencial.** Los hermanos se separan: uno avanza por un pasillo bloqueado por 3 puertas y el otro llega a una sala de control con palancas que abren y cierran esas puertas de forma combinada. Hay que alternar constantemente para abrir el paso.

### 🏁 Desenlace
Ambos caminos se unen en una escalinata de piedra oscura que sube hasta un altar donde la Muerte custodia el alma atrapada. Al llegar a la cima se detiene el temporizador y ganas la partida.

---

## 🖥️ Interfaz (HUD)

HUD minimalista para no romper la inmersión en primera persona:

- **Retrato dinámico** (esquina superior): muestra al hermano que controlas, rojo o verde, y cambia al instante al pulsar `Tab`.
- **Temporizador** (centro superior): cuenta atrás en formato `MM:SS` desde `20:00`. No se pausa con las interacciones ni con los cambios de escena.
- **Aviso contextual:** *"Pulsa [E] para interactuar"*, que solo aparece cuando el hermano activo está cerca de un elemento interactuable.

---

## 🎨 Diseño de arte y sonido

- **Estilo:** 3D *low-poly* de aristas marcadas y texturas limpias. Referencia visual: la limpieza geométrica de *Human: Fall Flat*.
- **Contraste:** el Mundo Real es natural, colorido y realista; el Infierno es oscuro, dominado por el naranja incandescente de la lava y las rocas oscuras.
- **Música:** piezas minimalistas de piano y cuerda que transmiten la soledad del plano espiritual y se aceleran en los últimos minutos del contador.
- **Efectos de sonido:** el chasquido de palancas y botones, los pasos y un sonido cristalino cada vez que el alma cambia de cuerpo.

---

## 💻 Programación

- **Cinemachine dinámico.** Control automático de prioridades de cámara (*vCam Priority*) para pasar limpiamente de primera a tercera persona durante la posesión.
- **Filtros por polaridad y tags.** Los *triggers* (`OnTriggerEnter`) comprueban si el tag del jugador (`Player1` / `Player2`) coincide con el color del material permitido.
- **Persistencia de datos globales.** Una clase estática `GameData` mantiene el temporizador de 20 minutos y el personaje activo entre escenas.
- **Renderizado condicional (asimetría).** Se activan o desactivan GameObjects según el hermano controlado para mostrar u ocultar los mapas de los carteles.

---

## 📁 Estructura del proyecto

```
Assets/
├── Scenes/
│   ├── EscenaInicial.unity       # Escenario 1: el Mundo Real
│   └── EscenaInfierno.unity      # Escenario 2: el Infierno
├── Scripts/                      # Lógica del juego (C#)
│   ├── CharacterSwitcher.cs      # Transición de alma (Tab)
│   ├── KidsController.cs         # Controlador de los hermanos
│   ├── GameData.cs, TimerHUD.cs  # Datos globales y temporizador
│   ├── PuzzleManager.cs, AN_Button.cs, AN_ButtonPuzzle.cs
│   ├── VisibilidadPorPersonaje.cs, PortalScene.cs, PortalParallax.cs
│   └── SpawnReset.cs, VictoriaTrigger.cs, DynamicMenuManager.cs, ...
├── Animations/ · Audio/ · Materials/ · Models/ · Prefabs/
└── Images/ · Sprites/ · Shaders/ · Textures/ · Settings/
```

### ▶️ Cómo abrir el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/franra18/Body-Hop-Game.git
   ```
2. Ábrelo con **Unity Hub**.
3. Abre la escena `Assets/Scenes/EscenaInicial.unity` y pulsa **Play**.

---

## 📦 Assets de terceros

| Asset | Autor | Uso |
|---|---|---|
| **Kids Character Free** | Sigmoid Button Assets | Modelos del Hermano Rojo y el Hermano Verde |
| **Stylized Low Poly Nature Lite** | JustCreate | Entorno natural del Mundo Real |
| **Inferno World Free** | ithappy | Rocas y lago de lava del Infierno |

---

## 🤖 Uso de inteligencia artificial

- **Gemini:** optimización y depuración de scripts (intercambio de cuerpos con `Tab`, persistencia de los cuerpos inertes y sincronización del temporizador) y apoyo en la dirección de arte y la música.
- **Unity AI:** generación y texturizado de *prefabs* clave: el **Portal del Infierno** y la **jaula final con el alma**.

---

## 👥 Equipo

**Dual Soul Games**

| Integrante | Roles |
|---|---|
| **Francisco Ramírez Cañadas** | Diseño de juego, Arte 3D, Sonido, Diseño de niveles y Producción |
| **Marcos Luque Montiel** | Diseño de juego, Programación, Interfaz/UI y Diseño de niveles |
