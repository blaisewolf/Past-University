import pygame
import pygame.gfxdraw
import shutil
import options
import scoreboard
import tetris
import graphics

def main_menu_keyup(event):
    """A főmenüben gomb felengedésre újrarajzolja a képernyőt."""
    if event.key == pygame.K_DOWN or event.key == pygame.K_UP:
        return True

def main_menu_keydown(event, menu):
    """A menüpont változót módosítja a főmenüben."""
    if event.key == pygame.K_DOWN:
        if menu == 4:
            menu = 1
        else:
            menu += 1
    if event.key == pygame.K_UP:
        if menu == 1:
            menu = 4
        else:
            menu -= 1
    return menu

def main_menu_enter(window, event, menu):
    """A főmenüben kezeli az enter gomb megyomását."""
    if event.key == pygame.K_RETURN:
        if menu == 1:
            tetris.tetris(window)
        if menu == 2:
            scoreboard.scoreboard(window)
        if menu == 3:
            options.options(window)
        if menu == 4:
            pygame.quit()

def options_keyup(event):
    """Az opciókban gombe felengedésre újrarajzolja a képernyőt."""
    if event.key == pygame.K_DOWN or event.key == pygame.K_UP or event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
        return True
    if event.key == pygame.K_RETURN or event.key == pygame.K_ESCAPE:
        return True

def options_keydown(event, menu, settings):
    """Beállítási adatok és menüpont változók változtatásáért felelős."""
    if event.key == pygame.K_DOWN:
        if menu.insize:
            if menu.setsize == 1:
                menu.setsize = 2
            else:    
                menu.setsize = 1
        elif menu.x == 2 and menu.y == 2:
            menu.y = 1
        elif menu.x == 1 and menu.y == 3:
            menu.y = 1
        else:
            menu.y += 1
    if event.key == pygame.K_UP:
        if menu.insize:
            if menu.setsize == 1:
                menu.setsize = 2
            else:    
                menu.setsize = 1
        elif menu.x == 2 and menu.y == 1:
            menu.y = 2
        elif menu.x == 1 and menu.y == 1:
            menu.y = 3
        else:
            menu.y -= 1
    if event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
        if event.key == pygame.K_LEFT and menu.inlvl:
            if settings.lvl == 0:
                settings.lvl = 9
            else:
                settings.lvl -= 1
        elif event.key == pygame.K_RIGHT and menu.inlvl:
            if settings.lvl == 9:
                settings.lvl = 0
            else:
                settings.lvl += 1
        elif event.key == pygame.K_RIGHT and menu.insize and menu.setsize == 1:
            if settings.width == 40:
                settings.width = 5
            else:
                settings.width += 1
        elif event.key == pygame.K_LEFT and menu.insize and menu.setsize == 1:
            if settings.width == 5:
                settings.width = 40
            else:
                settings.width -= 1
        elif event.key == pygame.K_RIGHT and menu.insize and menu.setsize == 2:
            if settings.height == 25:
                settings.height = 6
            else:
                settings.height += 1
        elif event.key == pygame.K_LEFT and menu.insize and menu.setsize == 2:
            if settings.height == 6:
                settings.height = 25
            else:
                settings.height -= 1
        elif menu.x == 1:
            menu.x = 2
            if menu.y == 3:
                menu.y = 2
        else:
            menu.x = 1
    return options.Menu(menu.x, menu.y, menu.inlvl, menu.insize, menu.setsize), options.Settings(settings.sound, settings.music, settings.lvl, settings.width, settings.height)

def options_enter(event, menu, settings):
    """Az enter gombot kezeli az opciókban"""
    if event.key == pygame.K_RETURN:
        if menu.x == 1 and menu.y == 1:
            if settings.sound:
                settings.sound = False
            else:
                settings.sound = True
        if menu.x == 1 and menu.y == 2:
            if settings.music:
                settings.music = False
            else:
                settings.music = True
        if menu.x == 1 and menu.y == 3:
            shutil.copyfile('Reset_Scoreboard.txt', 'Scoreboard.txt')
        if menu.x == 2 and menu.y == 1:
            if menu.inlvl:
                menu.inlvl = False
            else:
                menu.inlvl = True
        if menu.x == 2 and menu.y == 2:
            if menu.insize:
                menu.insize = False
            else:
                menu.insize = True
    return options.Menu(menu.x, menu.y, menu.inlvl, menu.insize, menu.setsize), options.Settings(settings.sound, settings.music, settings.lvl, settings.width, settings.height)

def tetris_K_LEFT(window, settings, block, board, counter, lvl, lines):
    """A tetrisben ez a függvény mozgatja balra a blokkokat.
    Lejátsza a mozgás hangefektet."""
    if tetris.check_xcollision_l(block, board, counter):
        pass
    else:
        counter.x -= 1
        move = pygame.mixer.Sound('Move.wav')
        pygame.mixer.Sound.play(move)
        graphics.tetris_screen_update(window, settings, board, block, counter, lvl, lines)
    return tetris.Counter(counter.x, counter.y)

def tetris_K_RIGHT(window, settings, block, board, counter, lvl, lines):
    """A tetrisben ez a függvény mozgatja jobbra a blokkokat.
    Lejátsza a mozgás hangefektet"""
    if tetris.check_xcollision_r(settings, block, board, counter):
        pass
    else:
        counter.x += 1
        move = pygame.mixer.Sound('Move.wav')
        pygame.mixer.Sound.play(move)
        graphics.tetris_screen_update(window, settings, board, block, counter, lvl, lines)
    return tetris.Counter(counter.x, counter.y)
