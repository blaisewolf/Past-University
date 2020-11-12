import pygame
import pygame.gfxdraw
import graphics
import controls
import options

def main():
    """ Főprogram"""
    pygame.init()

    window = pygame.display.set_mode((1280, 720))
    pygame.display.set_caption('Potential Tetris')

    menu = 1
    while True:
        draw = True
        if draw:
            graphics.main_menu_select(window, menu)
            draw = False
            pygame.display.update()
        event = pygame.event.wait()
        if event.type == pygame.KEYUP:
            draw = controls.main_menu_keyup(event)
        if event.type == pygame.KEYDOWN:
            menu = controls.main_menu_keydown(event, menu)
            controls.main_menu_enter(window, event, menu)
        if event.type == pygame.QUIT:
            pygame.quit()
main()
