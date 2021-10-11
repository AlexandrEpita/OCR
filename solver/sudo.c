#include <stdlib.h>
#include <stdio.h>

extern int Taille=10; //9 (chiffres) +1
extern int Taille_texte=111;//9x9 (chiffres) + 2x 9 (2 espaces par ligne) + 9+2 (retour a la ligne)  +1 (null)
struct sudoku
{
	int grille[Taille][Taille];//la grille de sudoku
	char s[Taille_texte];// le fichier version string (tout les fichiers ont le meme nb de caractere )
};
struct sudoku load_grile(char path[])
{
	FILE* fichier = NULL;
	fichier = fopen(path[], "r");
	if (fichier != NULL)
   	{
		struct sudoku sud;//initialisation
        	// On peut lire et écrire dans le fichier
		size_t i=0;
		do
        	{
            		cara = fgetc(fichier); // On lit le caractère
            		sud.s[i]=cara; // On l'affiche
			i++;
        	} while (caractereActuel != EOF and i<Taille_texte); // On continue tant que fgetc n'a pas retourné EOF (fin de fichier)
 
        	fclose(fichier);
    	}
    	else
   	{
        	// On affiche un message d'erreur si on veut
        	printf("Impossible d'ouvrir le fichier test.txt");
    	}
	return sud;
}
void update_grille(struct sudoku sud){
	int x=0,y=0;
	for(size_t i=0;sud.s[i]!=0;i++)
	{
		if(sud.s[i]=='.'){ s.grille[y][x]= 0 ; }
		if(sud.s[i]==' '){x--;}
		if(sud.s[i]=='\n'){y++;x=-1;}
		else{ s.grille[y][x]= (int){sud.s[i]-'0') ; }
		x++;
	}
}
	
void update_string(struct sudoku sud){
	size_t val=0;
	for(int y=0;y<Taille;y++)
	{
		for(int x=0;x<Taille;x++)
		{
			sud.s[val]=grid[y][x];
			val++;
			if(x==2 || x==5){sud.s[val]=' ';val++;}		
		}
		sud.s[val]='\n';val++;
		if(y==2 || y==5){sud.s[val]='\n';val++;}	
	}
	sud.s[val-1]=0;
}


int ouverture_fichier1(struct sudoku sud, char nom[])
{
	FILE* fichier = NULL;

	fichier = fopen(nom[], "w");
	if (fichier != NULL)
	{
		fputs(sud.s[], fichier);
		fclose(fichier);
	}
	return 0;
}


void solver(struct sudoku sud)
{}
