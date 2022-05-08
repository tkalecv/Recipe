using Firebase.Auth;

namespace Recipe.Auth
{
    public interface IFirebaseClient
    {
        FirebaseAuthProvider FirebaseAuthProvider { get; set; }
    }
}