import { CategoryModel } from './CategoryModel';

export class NewsModel {
    

    public Id: number = 0;

    public Title: string = null;

    public Lead: string = null;

    public Content: string = null;

    public CreateDate: Date = null;

    public ValidPeriod: number = 0;

    public Author: number = 0;

    public Category: CategoryModel[] = [];
}
