import pygame
import pygame.gfxdraw

def title_draw(window, text, size, color, y):
    """Középre igazított szöveget ír ki a képernyőre megadott szöveg, betűméret
    szín és magasság alapján"""
    font = pygame.font.SysFont('Arial', size)
    text = font.render(text, True, color)
    window.blit(text, ((1280 - text.get_width()) / 2, y))

def text_draw(window, text, size, color, x, y):
    """Szöveget ír ki a képernyőre megadott szöveg, betűméret,
    szín, magasság és szélesség alapján"""
    font = pygame.font.SysFont('Arial', size)
    text = font.render(text, True, color)
    window.blit(text, (x, y))

def main_menu_select(window, menu):
    """Kiírja a címet és a menüpontokat a képernyőre a főmenüben.
    A sárga kijelölés attól függ, mekkora a menüváltozó értéke."""
    white = pygame.Color('#FFFFFF')
    yellow = pygame.Color('#FFFF00')
    title_draw(window, 'Potential Tetris', 86, white, 100)    
    title_draw(window, 'New Game', 54, yellow, 320) if menu == 1 else title_draw(window, 'New Game', 54, white, 320)
    title_draw(window, 'Scoreboard', 54, yellow, 420) if menu == 2 else title_draw(window, 'Scoreboard', 54, white, 420)
    title_draw(window, 'Options', 54, yellow, 520) if menu == 3 else title_draw(window, 'Options', 54, white, 520)
    title_draw(window, 'Quit', 54, yellow, 620) if menu == 4 else title_draw(window, 'Quit', 54, white, 620)

def options_select(window, menu, settings):
    """Kiírja a képernyőre az "options" menüpontban lévő szövegeket.
    Számontartja a kijelölés színét a menüváltozók alapján."""
    white = pygame.Color('#FFFFFF')
    yellow = pygame.Color('#FFFF00')
    red = pygame.Color('#FF0000')
    green = pygame.Color('#008000')

    window.fill(pygame.Color('#000000'))
    title_draw(window, 'Options', 86, white, 100)

    text_draw(window, 'Sound', 40, yellow, 100, 250) if menu.x == 1 and menu.y == 1 else text_draw(window, 'Sound', 40, white, 100, 250)
    text_draw(window, 'Music', 40, yellow, 100, 320) if menu.x == 1 and menu.y == 2 else text_draw(window, 'Music', 40, white, 100, 320)
    text_draw(window, 'Reset Scoreboard', 40, yellow, 100, 420) if menu.x == 1 and menu.y == 3 else text_draw(window, 'Reset Scoreboard', 40, white, 100, 420)
    text_draw(window, 'Level', 40, yellow, 640, 250) if menu.x == 2 and menu.y == 1 else text_draw(window, 'Level', 40, white, 640, 250)
    text_draw(window, 'Game Size', 40, yellow, 640, 420) if menu.x == 2 and menu.y == 2 else text_draw(window, 'Game Size', 40, white, 640, 420)
    text_draw(window, ('On'), 40, green, 300, 250) if settings.sound else text_draw(window, ('Off'), 40, red, 300, 250)
    text_draw(window, ('On'), 40, green, 300, 320) if settings.music else text_draw(window, ('Off'), 40, red, 300, 320)
    text_draw(window, str(settings.lvl), 40, green, 1000, 250) if menu.inlvl else text_draw(window, str(settings.lvl), 40, red, 1000, 250)
    text_draw(window, 'Width', 40, yellow, 700, 530) if menu.insize and menu.setsize == 1 else text_draw(window, 'Width', 40, white, 700, 530)
    text_draw(window, 'Height', 40, yellow, 700, 630) if menu.insize and menu.setsize == 2 else text_draw(window, 'Height', 40, white, 700, 630)
    text_draw(window, str(settings.width), 40, green, 1000, 530) if menu.setsize == 1 and menu.insize else text_draw(window, str(settings.width), 40, red, 1000, 530)
    text_draw(window, str(settings.height), 40, green, 1000, 630) if menu.setsize == 2 and menu.insize else text_draw(window, str(settings.height), 40, red, 1000, 630)

def scoreboard_draw(window, data):
    """Kiírja a ranglistát a "Scoreboard" menüpontban. Név, pályaméret, pont"""
    data = data[::-1]
    white = pygame.Color('#FFFFFF')
    for x in range(len(data)):
        text_draw(window, data[x].name, 30, white, 200, 200 + x*30)
        text_draw(window, data[x].size, 30, white, 600, 200 + x*30)
        text_draw(window, data[x].points, 30, white, 1000, 200 + x*30)

def get_color(lvl):
    """Váltohatja a blokkok színét a játékban attól függően
    hanyas szinten van a játékos."""
    white = pygame.Color('#FFFFFF')
    yellow = pygame.Color('#FFFF00')
    red = pygame.Color('#FF0000')
    green = pygame.Color('#008000')
    orange = pygame.Color('#FFA500')
    blue = pygame.Color('#0000FF')
    purple = pygame.Color('#800080')
    brown = pygame.Color('#A52A2A')
    pink = pygame.Color('#FFC0CB')
    violet = pygame.Color('#8A2BE2')
    color = None
    if lvl // 10 == 0:
        color = white
    elif lvl // 10 == 1:
        color = yellow
    elif lvl // 10 == 2:
        color = red
    elif lvl // 10 == 3:
        color = green
    elif lvl // 10 == 4:
        color = orange
    elif lvl // 10 == 5:
        color = blue
    elif lvl // 10 == 6:
        color = purple
    elif lvl // 10 == 7:
        color = brown
    elif lvl // 10 == 8:
        color = pink
    elif lvl // 10 == 9:
        color = violet
    return color
    

def board_draw(window, settings, board, lvl):
    """Megrajzolja a pályát. Ha az adott mezőn nincs blokk akkor fehér, ha van akkor színes."""
    for i in range(len(board)):
        for j in range(len(board[i])):
            color = pygame.Color('#A9A9A9') if board[i][j] == 0 else get_color(lvl)
            pygame.gfxdraw.box(window, pygame.Rect(((1280 / 2) - (settings.width*15 // 2) + 15*j), 200 + i*15, 15, 15), color)

def block_draw(window, settings, block, counter, lvl):
    """Megrajzolja a kiválasztott blokkot."""
    color = get_color(lvl)
    for i in range(len(block)):
        for j in range(len(block[i])):
            if block[i][j] == 1:
                pygame.gfxdraw.box(window, pygame.Rect(((1280 / 2) - (settings.width*15 // 2) + 15*j + counter.x*15), 200 + counter.y*15 + i*15, 15, 15), color)

def lines_draw(window, lines):
    """Kiírja a képernyőre hány sikeresen kirakott sort ért el a játékos."""
    white = pygame.Color('#FFFFFF')
    text = "Lines: " + str(lines)
    title_draw(window, text, 28, white, 100)

def tetris_screen_update(window, settings, board, block, counter, lvl, lines):
    """Ez a függvény foglalja egybe az összes képernyőre kikerülő adatot a Tetrisben"""
    window.fill(pygame.Color('#000000'))
    lines_draw(window, lines)
    board_draw(window, settings, board, lvl)
    block_draw(window, settings, block, counter, lvl)
    pygame.display.update()
