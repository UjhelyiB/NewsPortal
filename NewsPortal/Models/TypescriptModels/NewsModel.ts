
export class NewsModel {
    

    public Id: number = 0;

    public Title: string = null;

    public Lead: string = null;

    public Content: string = null;

    public CreateDate: Date = null;

    public ValidPeriod: number = 0;

    public Author: string = null;

    public CategoryIds: number[] = [];
}
