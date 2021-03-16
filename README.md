# Unity EasySoundPlayer

Unityで簡単に使えるBGM・SE再生用アセットです。

初心者の方でも手順通り導入すれば簡単に使えることを目標に作りました。  
「できること」以上のことを求める場合は逆に使いづらいと思うので自作するか別のアセットを探してください。

## できること

* BGMの再生／停止／再開
* BGMのループ再生
* SEの再生（複数同時再生）
* BGM/SEの音量変更
* 音量変更UIの作成補助機能

## できないこと

* 複数のBGMファイルのループ再生やランダム再生
* BGMのクロスフェード再生
* オーディオミキサーの利用
* など

# 動作環境

* Unity 2019.4(LTS)以上

# 導入方法

### UniRxの導入

一部UniRxを使用しているため先に導入します。  
すでに別の方法（unitypackage等）を利用してUniRxを導入している場合、一度消す必要があります。わからない場合は諦めてください。

1. Window -> Package Manager
    * <img width="331" alt="スクリーンショット 2021-03-17 1 04 29" src="https://user-images.githubusercontent.com/7110482/111341584-ff4ad500-86bc-11eb-9a50-ba852e0b5d89.png">
1. "+" -> Add package from git URL...
    * <img width="357" alt="スクリーンショット 2021-03-17 1 07 16" src="https://user-images.githubusercontent.com/7110482/111341908-4e910580-86bd-11eb-9e0c-6eba31f53281.png">
1. 以下のURLを入力し、Addを押す 
    * `https://github.com/neuecc/UniRx.git?path=Assets/Plugins/UniRx/Scripts`
1. UniRxが導入済みとなれば成功
    * <img width="768" alt="スクリーンショット 2021-03-17 1 10 08" src="https://user-images.githubusercontent.com/7110482/111342221-96b02800-86bd-11eb-8692-8b117f097c22.png">

### EasySoundPlayer(本アセット)の導入

UniRxと同様の手順で、以下のURLを使って導入してください。

`https://github.com/naichilab/unity-easy-sound-player.git?path=Assets/naichilab/EasySoundPlayer`

こちらもリストに表示されれば成功です。

<img width="768" alt="スクリーンショット 2021-03-17 1 13 20" src="https://user-images.githubusercontent.com/7110482/111342654-00303680-86be-11eb-8801-3dcdbc772112.png">

# 使い方

## 準備

1. プロジェクトビューにて、`Packages/EasySoundPlayer/`に色々なファイルが存在することを確認します。
    * <img width="317" alt="スクリーンショット 2021-03-17 1 15 06" src="https://user-images.githubusercontent.com/7110482/111342979-4e453a00-86be-11eb-95d8-1b826d38fdcc.png">
    * もし存在しない場合は導入失敗していますので手順を再確認ください。
1. プロジェクトビューの `Packages/EasySoundPlayer/Prefabs/` にある２つのプレハブをシーンに配置します。（ヒエラルキービューにドラッグ＆ドロップ）
    * <img width="606" alt="スクリーンショット 2021-03-17 1 17 06" src="https://user-images.githubusercontent.com/7110482/111343221-8d738b00-86be-11eb-96f2-edc4e50e4b70.png">
1. プロジェクトビューの `Assets/` 以下に、使用するBGMやSEのオーディオファイルを配置します。
    * フォルダ名はなんでも良いです。
    * <img width="332" alt="スクリーンショット 2021-03-17 1 19 33" src="https://user-images.githubusercontent.com/7110482/111343550-e0e5d900-86be-11eb-9ae8-42850cd7d3db.png">
1. ヒエラルキービューにて `BgmPlayer` を選択し、インスペクタからオーディオファイルをセットします。
    * <img width="976" alt="スクリーンショット 2021-03-17 1 23 26" src="https://user-images.githubusercontent.com/7110482/111344339-a29ce980-86bf-11eb-95b1-9b1d92f2cccf.png">
1. 同様の手順で `SePlayer` の方にもオーディオファイルをセットします。
    * <img width="977" alt="スクリーンショット 2021-03-17 1 26 46" src="https://user-images.githubusercontent.com/7110482/111344633-e132a400-86bf-11eb-8d08-779013b57cde.png">
   
## BGMの再生方法

* BgmPlayerのインスペクタで `PlayOnAwake` をオンにしておくとシーン読み込み完了時に自動的に再生開始されます。  
    * 複数のオーディオファイルをセットした場合は１つ目が利用されます。
    * <img width="462" alt="スクリーンショット 2021-03-17 1 35 05" src="https://user-images.githubusercontent.com/7110482/111345905-0a076900-86c1-11eb-96df-7f57a094e3f3.png">
   
スクリプトから手動で鳴らしたい場合や、曲を変更したい場合は以下のコードを書いてください。

```.cs
// BGM再生方法A：ファイル番号を指定して再生する
BgmPlayer.Instance.Play(0);
// BGM再生方法B：ファイル名を指定して再生する
BgmPlayer.Instance.Play("bgm_maoudamashii_fantasy03");
// BGMを一時停止
BgmPlayer.Instance.Pause();
// BGMを停止
BgmPlayer.Instance.Stop();
```

## SEの再生方法

SEを鳴らしたい時に、以下のコードを書いてください。

```.cs
// SE再生方法A：ファイル番号を指定して再生する
SePlayer.Instance.Play(0);
// SE再生方法B：ファイル名を指定して再生する
SePlayer.Instance.Play("se_maoudamashii_system28");
```


## 音量調整UIの作成

1. ヒエラルキービューで、`Slider` を２つ作成します。（BGM/SEそれぞれ用）
    * <img width="274" alt="スクリーンショット 2021-03-17 1 37 21" src="https://user-images.githubusercontent.com/7110482/111346204-5783d600-86c1-11eb-9543-d4920b0769dd.png">
1. ２つのSliderに分かりやすい名前をつけます。
    * <img width="256" alt="スクリーンショット 2021-03-17 1 38 45" src="https://user-images.githubusercontent.com/7110482/111346506-9e71cb80-86c1-11eb-99d1-4b2045f6c93d.png">
    * ここでは `BgmVolumeSlider` と `SeVolumeSlider` としました。
1. 位置や見た目は自由に編集してください。
    * Unity標準のuGUIという仕組みなので、わからない場合はググってください。
    * 例なので適当に位置だけ調整しました。
    * <img width="498" alt="スクリーンショット 2021-03-17 1 41 15" src="https://user-images.githubusercontent.com/7110482/111346768-e5f85780-86c1-11eb-897d-a9f585a4e71d.png">
1. BgmPlayerのインスペクタを開き、作成したSliderをセットします。
    * <img width="975" alt="スクリーンショット 2021-03-17 1 42 00" src="https://user-images.githubusercontent.com/7110482/111346945-13450580-86c2-11eb-8020-cef8bf4e880f.png">
1. SePlayerの方にも同様にセットします。
    * <img width="470" alt="スクリーンショット 2021-03-17 1 42 56" src="https://user-images.githubusercontent.com/7110482/111347080-32439780-86c2-11eb-8b24-cd122691092c.png">
   
以上で完了です。

実行すればスライダーを操作することで音量が変更できます。  
SEの方は、変更するたびに１つ目のSEが鳴ります。

もしスクリプトから音量を変更したい場合は以下のように書いてください。

```.cs
// BGM音量を変更（0〜1）
BgmPlayer.Instance.Volume = 0.5f;
// SE音量を変更（0〜1）
SePlayer.Instance.Volume = 0.5f;
```

# ライセンス

* MITライセンス
* This library is under the MIT License.
