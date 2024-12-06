export interface FamilyTreeState {
  countryList: any[],
  person: any,
  loading: {
    showLoading: boolean, text: string
  }
}


export const familyTreeInitialState: FamilyTreeState = {
  countryList: [],
  person: null,
  loading: {
    showLoading: false,
    text: 'Cargando'
  }
}