#include <stdio.h>
#include <stdlib.h>
#include <sudo.h>


int main(int argv ,*argv[])
{
	if(argc !=2) {
		printError( argv[0], "number of args invalid" ); 
		return EXIT_FAILURE; 
 	}
	char *filename = NULL;
	filename=argv[0];
	size_t i;
    	struct sudoku sudo=load_grille( filename);

	//test 
	printf("la file en param contient:%s \n",sudo.s[]);	

	update_grille(sudo);
	
	//test
	printf("la grille  contient: \n");	
	for(i=0;i<Taille;size_t++)
	{printf("%i",sudo.grille[i]);sudo.grille[i]=1;}

	solver(sudo);
	update_string(sudo);
	
	for(i=0;filename[i]!=0;i++;){}//calcul taille du nom de la file txt ex: grid_00
	size_t i2=i+7+1;//le nom du fichier + ".result" = le null
	char add[]=".result"
	char name[i2];// ex: grid_00.result
	for (i=0;i<i2-7;i++)
	{name[i]=filename[i];}
	i++;
	for(size_t i3=0;i<i2;i3++)
	{name[i]=add[i3];i++;}
	ouverture_fichier(sudo,name[]);
	
	
	
	
	return 0;
}