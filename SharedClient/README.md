- EndPoints
  Define Url

- Interfaces API
	IWebManager : Interfaces base. dung de tuong tac truc tiep voi API thong qua (get,post,put,delete)

- Managers API 
	Implement cua Interfaces

- Entities
  Entities de map du lieu tra ve tu API

- Models
  Entities de show du lieu len GUID

- ViewModel
	Class trung gian giua GUID va Manager. giong nhu Controller trong Angularjs

- App 
  Register Implement cho Interfaces tuong ung
  Property cho tat ca Interfaces. co the truy suat bat ky o dau




  GUID(ios,android,winphone) -> ViewModel(Models,Interfaces) -> Interfaces(Managers) - API