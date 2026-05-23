# Tareas

## FRAN
- Crear escenario de alma encerrada
- Anadir sonidos/musica
- Crear presentacion/video introductorio
- Carteles prueba 1 infierno

### Detalles finales
- Limpiar carpeta Assets
- Anadir sangre y humo de coche en la escena inicial

## MARCOS
- Mejorar resolucion imagen usuario

### Detalles finales
- Flecha que te indique direccion del otro personaje

# ENTREGABLES

## Documento - Hecho

## Presentación

Se puede usar un documento (tipo PowerPoint o similar) que sirva de guía para mostrar lo realizado:

- [ ] Describir los assets utilizados
- [ ] Mostrar pantallazos del juego
- [ ] Comentar aspectos de la programación
- [ ] Presentar el diseño del juego
- [ ] Señalar aspectos relevantes del proyecto

## Tráiler

- [ ] Un tráiler/vídeo que muestre el funcionamiento del juego.




## COSAS A TENER EN CUENTA PARA ACABAR EL JUEGO

Al modificar AudioListener.volume, el ajuste se aplica automáticamente a todos los sonidos del juego sin necesidad de tocar nada más.

Para acceder a esa variable desde cualquier otro script, debes usar PlayerPrefs.GetFloat con el mismo nombre exacto que le pusiste ("VolumenJuego"). Se hace con esta línea:

float volumenActual = PlayerPrefs.GetFloat("VolumenJuego", 1f);

El 1f del final es el valor por defecto que te devolverá si el jugador acaba de instalar el juego y aún no ha movido la barra de volumen.

---------------------------

Para la Victoria: En el script donde el personaje cruza el portal final o resuelve el último puzzle, añade:

FindObjectOfType<DynamicMenuManager>().MostrarMenu(DynamicMenuManager.MenuState.Victoria);