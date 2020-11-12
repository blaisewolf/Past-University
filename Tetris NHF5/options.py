import pygame
import pygame.gfxdraw
import graphics
import controls

class Settings():
    def __init__(self, sound, music, lvl, width, height):
        self.sound = sound
        self.music = music
        self.lvl = lvl
        self.width = width
        self.height = height

class Menu():
    def __init__(self, x, y, inlvl, insize, setsize):
        """Létrehozza a menüponthoz szükséges osztályt.
        Számontartja az x tengelyt, az y tengelyt, azt hogy éppen állítjuk-e
        a szint vagy méret menüpontot."""
        self.x = x
        self.y = y
        self.inlvl = inlvl
        self.insize = insize
        self.setsize = setsize
        

def str_to_bool(string):
    """Sztringet konvertál boolra."""
    if string == 'True':
        return True
    elif string == 'False':
        return False

def settings_rt():
    """Beolvassa a Settings fájlból a játék beállításait."""
    output = []
    with open('Settings.txt', "rt") as f:
        for line in f:
            line = line.rstrip("\n")
            temp = line.split("\t")
            output.append(temp[1])
    return Settings(str_to_bool(output[0]), str_to_bool(output[1]), int(output[2]), int(output[3]), int(output[4]))

def settings_wt(settings):
    """Elmenti a játékbeállításokat egy txt fájlba."""
    with open('Settings.txt', "wt") as f:
        f.write("sound\t{}\nmusic\t{}\nlvl\t{}\nwidth\t{}\nheight\t{}".format(settings.sound, settings.music, settings.lvl, settings.width, settings.height))
        
def options(window):
    """Az options főfüggvénye."""
    settings = settings_rt()
    menu = Menu(1,1, False, False, 1)

    draw = True
    while True:
        if draw:
            graphics.options_select(window, menu, settings)
            pygame.display.update()
            draw = False
        event = pygame.event.wait()
        if event.type == pygame.KEYUP:
            draw = controls.options_keyup(event)
        if event.type == pygame.KEYDOWN:
            menu, settings = controls.options_keydown(event, menu, settings)
            menu, settings = controls.options_enter(event, menu, settings)
            if event.key == pygame.K_ESCAPE:
                if not menu.inlvl and not menu.insize:
                    window.fill(pygame.Color('#000000'))
                    break
        if event.type == pygame.QUIT:
            pygame.quit()
    settings_wt(settings)
