import { CategoryModel } from './CategoryModel';

export class NewsModel {
    

    public Id: number = 0;

    public Title: string = null;

    public Context: string = null;

    public Category: CategoryModel[] = [];
}
