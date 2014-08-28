using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace wcat
{
    class Program
    {
        static void Main(string[] args)
        {   
            /*******                    response decryption     **************/
            //first time
            Console.WriteLine(GzUncompress(Cipher.DecryptRJ128Byte(
                Cipher.DEFAULT_NETWORKHASH,
                Cipher.DEFAULT_IV_128,
                "MPOoBl31VhdEZYTkh4XsQ8bo0EsdtmNdS229zt7iibXO15yXqMsNzaR0e8wzSFsvxiTvR8CjHxTMzMD9yUcNQQ=="
            )));
           
            //otherwise

            //userHash is stored in shared_prefs using CryptoPrefs
            //<string name="md5('Account')">{"token":"wcatpt=xxx","userHash":"yyy","mailAddress":"","gmailAddress":""}</string>
            Console.WriteLine(CryptoPrefs.Decrypt("yy5spAe2Vhruecg7Zd+vm6X/KsIi/5MKVlxX9gjnBgTpd0th4eqqNM8Q0hI4gspBQd/7kW5Oa+6r8jJqR1k/YkEI5wFc1DxYwX3i7EhLfxWVv6csWaxnHDu7et7KzR4+r3Bra4W3bxcjE/p4DwK/+/CCeDzlW7Kn/O/Jn4do51NxgWIAs5U9zrcr0383qRsucXdVUAPEH3X/HpJdQNye4Gefl3FYOeeiREFQdlz5UWKysPMBhTjlaK5DTEEdbVz76ZM6BiMSvkUfJmCh6CC81xNbgaZ6GV962hl8uMQfynJcKSdRTq2swT8T/d/rh+/q"));
            Console.WriteLine(CryptoPrefs.Decrypt("W2lmvAavuGNYBBkxvCM3ubQP6ijTIan4jN8fYBnoPkv625Fi8kp48zhLn0a6ky0Jt7BVI8d+l9E8VCR0TqNvn9X01e0A4sKSLHKL8muayqhAGIPH03mo5TZh3p8b5ujM82/GJGk922DqGhOKgd9EPGoWgiuxKLxAPBhz/71rWVIVq5UliXLTpy2VFlRVqlYC9anAdJyXqEne/bjim5FbbZDSpeXWcDtYgVxxbZA69sejchju6PJ9K4P1KBEM268LxvYEudiretA1TtZ1XA9kdWhK0AisimeY5d5C/k72G8DcUPXRPaZesagqF4II/rnFOv1b2Aps25tUZrjgWlOlRvHIRGWAipsIbcG4ZYe5+TH25ALOExCXXY5LNx/epQVDtcPOa3Mqk0cpXnjDPvbX3IYEUoafyoCZjYJXo+hBIMD0IeX1b087aIvyB/oIduWYAwY9juwfJbImkJQmHswDNEJjOFQTHgte3DO+/STSyFtpF4kFHr1FJn2qiUqLV6xIFfx00H8RwWn4rADCsfFjutybDF8PhwuGcXxreQt09oawapxaXkq8uLabUwcTuUXSzN4u/WsYe2gNcTG9Szo0fT8ixJQ03QrcJ8a/gr4pKH+19d2vBquOsFR/RZz0idBmE9esZq6vHIqyVmJkdt0JW8LxUwDGdL7uK7jBcgZNJNKvojzUtgRfIc/JbgnLNWEOau5JqgnGyaocT+xdN5tH6QnTfj839HiWwYkHs7addnlO2facMuzznL60F+FOV/U1Hdmik4zk3Q7Ad1te6kPaJvIkd/aJngYu2TA1S6efZj1w3aWOpajDbUJKltdenpAs"));
            Console.WriteLine(CryptoPrefs.Decrypt("J3MGeRhm+b0HfU8fdB+BI62b4n/zRlQzMODfePSIDcwRjFsMqob224xIzfLY6kWHgm8/AZHVlJzJe1SaxoKPwdGbBdSxdW4SAx0GCpddSr19owqWxntGmUl9zxOyW35u08hDRntozrO0e8sSZe8dteOkTJTwJd3UzXUy89V5fCBZUaouSNd1lwi0vzRn/a4zcJeQjICYOcGu1ur7iJ8/1edmrEtJjhTtpbYXvI37k+lB1mpxKYX+Atu0sb1gsr86KEKVLzky4joiKTZ4G+xpSw=="));
            //use token as cookie to login
            //use userHash as Cipher.key
            Console.WriteLine(GzUncompress(Cipher.DecryptRJ128Byte(
                 "690828c9082a6fcdfce9a910d2dfd028",//userHash
                 Cipher.DEFAULT_IV_128,
                 "jUL/x5jT4+anfwTcIjdFLh4YmFiK5g3Q6KU2L/lcU8VarJwLERC3RwfMbPrv2DEpHu27qJ1jy/cuqAT9jFAN9I8YNP0d3b/ihFFQJ3oKSiCmITyRsN0DKeYLhNLeglxp6H5Ap9Zc9kKOqlGNkBKL4YZgMxuiUFHkzeaQj8IJS0fDMvx5ozZUQZTLIuub99tBzQ10G6GtDqfGAHW4QBLVgksOPJU8Uj/RoDXGaaI+uSVf6DS3/i+aA06nAy8M33vvhWXgZwAbenBoBVompupLGnroM0JQxpOqAVpaTBwbL8GYFDzw/P+ruPJOnEZTlh5h1S14dK5E6+XN86Xug/sZlGCMHGdClmF8/YKUubZeZpWez/gKbC/afTuGtsZRFu6FOGUloK1oCLRncpHeSHJCBmg44pu1UjQ0h7Q0LWL1MN2Th8VvUx3QwtVPnXxDrrajWzoZZLPBxf2jlL7Rs4DP/9b3D94IHQ4K6CmmqU87MeBh7wFhkKgPw90vjVkM/exgeqhVZ8qybVDBWefKH8ChQgvdNdvFMGhW4fn4NF4AJ2cf8tzobcIlHM9De1+tFsO+E7gKntEjlOxjyPQrcj/o6H4TrAKwJ5fDzlKcnVeeoAgPYmRw3oHdLaDOAFxjY92901xGDcIPzqcYHB7Fp49rj/AYjcO7tY6BKLka/wO8GwhoO4/nKP6yGvCQqq9yEOQWIAKNIveGtJ5umj8TErMOl7ma3NX9PLYEj9+UrXgv/WRWxQk8/qjNIqx5eSYttwBpYnVw8bRfQUMwtlBsgLPvDpMUMdayADUIRf6P0oxe+T1JgF7NecaFlbf1p7qP6D5Uznq1fsjMPkCdFWZFI8i0Mlsk61xIERcwVWHY/zrZfPJ8Ejjh3gqm/8XoOiZp4XqPznUj3/u5wVz/u9+jVsNKIQtw/sWgAsZyvEJFQta+WxB3VTtcy9pq6HljKbz6zrYSrczO2Xo6DA03vsYEJp+szlwbWHyDwkDzVGRO831YSLer4N8380tS93duXP6T1NHJ66o5S+gEQwGcm24F+VDNTnumeuMsJ4+giSXFSRjoMt4qJFbwkOiOuXpsD4PKG9JWzA2j/JqLxb5M+gM62u14y0IZgqfoh5X0nYTOqJS9gnlb/LrWfa5f6b6nuZxeB9578C/WCJnWP2Qgs7zeAxcfg2QMs6DMiXE+FSkbDcvgAVp/vLuSS0yEzDC0++p+OmDo9rU+C3WBqMI25dFsieCcafRvRzS9KdE0IEVkaUNQJdkvI1r9RKtbNcdJftDixlUtrakspOqi0GgPPUofF9hGhMrFu8uvK5cYO+E808029JRyofG342hZ9T6q7jfbWtIUB8YsYjA7e1c823BvQGS4HDZhIze7pupjiWLKa6Ghluu1g9fbKH1a/JCEW4P2EPKu43S538rY+y+xfIdrHdrHPiS8iXv+n0akDeG23vZzDXLqTunb4V3pl28jhw0I0Hrv+sT7FsopyU3ZutCVzG5FuQuUGLDXuuje3q1Vwvwa5uWsTQvMPyN0bISiMfnoe8x1gZ00L/3cD/P9bGFfQLQL2+swUTNl6oCpaRgUj8bTpbHhcsoRnj71Q1L+dFH1TzOvcothjimUok3yD7MkblSpOlf7witJ1eqk59U8GDHI4AmvBsUN7WPn79zqbjy0HEIAiT6BQHkZZXcBfK5xm74jJcD9y8d+CfEveJ5UhYVR9gnthsA/efEaTpO4/sJ/OZhEcwUOWzI14BsYTu/dPIUvfyeq5kr6qHf0l3IMvn384MKnuRd6UERQVcHqsVnisNN99c3StrMu75Hk7QhNpxJa2SnjVBmjckxFXJwajwmOb0tyiw4Pl+H4Awt0R5Ui+SfxKd1s2mFzv++A/ZCzOfe8E/pB8n+h7ENl7rjY/281sXIERLTNyfQaLm9i+iUmzSp0Jn53zW0UeydLNXfbrQneQwGikM2IsKmS4vCO9tPDOm9jgjiptU9iJ7ogPKSj7DYtmgwwNMfk1+BZ3zGQnKDbZ8oJdAY7SEid0mds4v1O862FZlfVr4JUlHTaBdWgreGkPkCSoHvvnZdC4iUP2BWkx+/BWZA7l+F+ye44XT8YqtL2kfp26vvZAJX2ObX7QmRpW0LKfs2kFXHiLfkJOWeVuwdfSh/qra3D9gTQlRcFVexL/OiZY+UF7nbzk1NYVtoV"
            )));

            /*******                    response signature     **************/ 
            // in Cipher.verify

            Console.ReadLine();
        }

        public static string GzUncompress(byte[] gzEncrypted)
        {
            byte[] bytes = ZlibStream.UncompressBuffer(gzEncrypted);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
