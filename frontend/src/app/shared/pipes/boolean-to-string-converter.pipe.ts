import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'booleanToStringConverter' })
export class BooleanToStringConverterPipe implements PipeTransform {

  private readonly values = new Map<string, string>();

  constructor() {
    this.values.set('true', 'Sim');
    this.values.set('false', 'Não');
  }

  transform(value: boolean, isLowerCase = true): string {
    let response = this.values.get(value.toString().toLowerCase());
    response = !!response ? response : '';
    return isLowerCase
      ? response
      : response.toUpperCase();
  }
}
